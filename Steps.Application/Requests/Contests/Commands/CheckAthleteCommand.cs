using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;

namespace Steps.Application.Requests.Contests.Commands;

public record CheckAthleteCommand(Guid AthleteId, Guid ContestId, bool isAppeared) : IRequest<Result>;

public class CheckAthleteCommandHandler : IRequestHandler<CheckAthleteCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CheckAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CheckAthleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var athleteRepository = _unitOfWork.GetRepository<Athlete>();
            var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
            var generatedListRepository = _unitOfWork.GetRepository<GeneratedAthletesList>();
            var lateAthletesListRepository = _unitOfWork.GetRepository<LateAthletesList>();

            // Получаем спортсмена по идентификатору
            var athlete = await athleteRepository.GetFirstOrDefaultAsync(
                a => a.Id == request.AthleteId,
                null,
                null,
                TrackingType.Tracking,
                false,
                false);

            if (athlete == null)
                return Result.Fail("Спортсмен не найден.");

            // Получаем групповой блок, в котором состоит спортсмен
            var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
                gb => gb.Athletes.Any(a => a.Id == athlete.Id),
                null,
                gb => gb.Include(g => g.Athletes),
                TrackingType.Tracking,
                false,
                false);

            if (groupBlock == null)
                return Result.Fail("Блок спортсмена не найден.");

            if (request.isAppeared)
            {
                athlete.IsAppeared = request.isAppeared;

                var contestId = groupBlock.ContestId;

                var generatedList = await generatedListRepository.GetFirstOrDefaultAsync(
                    gl => gl.ContestId == contestId,
                    null,
                    gl => gl.Include(gl => gl.GroupBlocks)
                        .ThenInclude(gb => gb.Athletes),
                    TrackingType.Tracking,
                    false,
                    false);

                if (generatedList == null)
                {
                    generatedList = new GeneratedAthletesList
                    {
                        ContestId = contestId,
                        GroupBlocks = new List<GroupBlock>()
                    };

                    var allGroupBlocks = await groupBlockRepository.GetAllAsync(
                        predicate: gb => gb.ContestId == contestId,
                        orderBy: null,
                        include: null,
                        disableTracking: false,
                        ignoreQueryFilters: false);
                    if (allGroupBlocks != null && allGroupBlocks.Any())
                    {
                        generatedList.GroupBlocks.AddRange(allGroupBlocks);
                    }

                    await generatedListRepository.InsertAsync(generatedList);
                }
                else if (generatedList.GroupBlocks == null || !generatedList.GroupBlocks.Any())
                {
                    var allGroupBlocks = await groupBlockRepository.GetAllAsync(
                        predicate: gb => gb.ContestId == contestId,
                        orderBy: null,
                        include: null,
                        disableTracking: false,
                        ignoreQueryFilters: false);
                    if (allGroupBlocks != null && allGroupBlocks.Any())
                    {
                        generatedList.GroupBlocks.AddRange(allGroupBlocks);
                    }

                    generatedListRepository.Update(generatedList);
                }

                var orderedBlocks = generatedList.GroupBlocks.OrderBy(gb => gb.ExitTime).ToList();

                if (!orderedBlocks.Any())
                {
                    return Result.Fail("Групповые блоки не сформированы.");
                }
            }
            else
            {
                // Помечаем спортсмена как неявившегося
                athlete.IsAppeared = request.isAppeared; // false
                athleteRepository.Update(athlete);

                // Получаем идентификатор конкурса из блока спортсмена
                var contestId = groupBlock.ContestId;

                // Получаем сформированный список спортсменов для конкурса
                var generatedList = await generatedListRepository.GetFirstOrDefaultAsync(
                    gl => gl.ContestId == contestId,
                    null,
                    gl => gl.Include(gl => gl.GroupBlocks)
                        .ThenInclude(gb => gb.Athletes),
                    TrackingType.Tracking,
                    false,
                    false);

                // Если список не существует или не содержит блоков, заполняем его
                if (generatedList == null)
                {
                    generatedList = new GeneratedAthletesList
                    {
                        ContestId = contestId,
                        GroupBlocks = new List<GroupBlock>()
                    };

                    var allGroupBlocks = await groupBlockRepository.GetAllAsync(
                        predicate: gb => gb.ContestId == contestId,
                        orderBy: null,
                        include: null,
                        disableTracking: false,
                        ignoreQueryFilters: false);

                    if (allGroupBlocks != null && allGroupBlocks.Any())
                    {
                        generatedList.GroupBlocks.AddRange(allGroupBlocks);
                    }

                    await generatedListRepository.InsertAsync(generatedList);
                }
                else if (generatedList.GroupBlocks == null || !generatedList.GroupBlocks.Any())
                {
                    var allGroupBlocks = await groupBlockRepository.GetAllAsync(
                        predicate: gb => gb.ContestId == contestId,
                        orderBy: null,
                        include: null,
                        disableTracking: false,
                        ignoreQueryFilters: false);

                    if (allGroupBlocks != null && allGroupBlocks.Any())
                    {
                        generatedList.GroupBlocks.AddRange(allGroupBlocks);
                    }

                    generatedListRepository.Update(generatedList);
                }

                // Сортируем групповые блоки по времени выступления
                var orderedBlocks = generatedList.GroupBlocks.OrderBy(gb => gb.ExitTime).ToList();

                if (!orderedBlocks.Any())
                {
                    return Result.Fail("Групповые блоки не сформированы.");
                }

                // Находим индекс блока, в котором состоит неявившийся спортсмен
                var absentBlockIndex = orderedBlocks.FindIndex(gb => gb.Athletes.Any(a => a.Id == athlete.Id));
                if (absentBlockIndex < 0)
                {
                    return Result.Fail("Групповой блок для спортсмена не найден в списке.");
                }

                // Удаляем неявившегося спортсмена из найденного блока
                var targetGroupBlock = orderedBlocks[absentBlockIndex];
                targetGroupBlock.Athletes.RemoveAll(a => a.Id == athlete.Id);
                groupBlockRepository.Update(targetGroupBlock);

                // Для всех блоков, следующих за блоком с неявившимся спортсменом,
                // смещаем время выступления на 2 минуты раньше
                for (int i = absentBlockIndex + 1; i < orderedBlocks.Count; i++)
                {
                    orderedBlocks[i].ExitTime = orderedBlocks[i].ExitTime.AddMinutes(-2);
                    groupBlockRepository.Update(orderedBlocks[i]);
                }

                // Добавляем спортсмена в список неявившихся
                var lateAthleteEntry = new LateAthletesList
                {
                    ContestId = contestId
                };
                
                lateAthleteEntry.Athletes.Add(athlete);
                
                await lateAthletesListRepository.InsertAsync(lateAthleteEntry);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok().SetMessage("Спортсмен отмечен, расписание обновлено.");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Ошибка при обновлении статуса спортсмена: {ex.Message}");
        }
    }
}