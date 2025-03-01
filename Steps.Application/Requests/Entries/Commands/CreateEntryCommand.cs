using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Application.Requests.Entries.Commands;

public record CreateEntryCommand (CreateEntryViewModel Model) : IRequest<Result<Guid>>;

public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var entry = _mapper.Map<Entry>(model);
        
        List<Athlete> athletes = new List<Athlete>();
    
        var entryRepository = _unitOfWork.GetRepository<Entry>();
        var contestRepository = _unitOfWork.GetRepository<Contest>();
        var entryAthletesRepository = _unitOfWork.GetRepository<EntryAthletesList>();
        var userRepository = _unitOfWork.GetRepository<User>();
        var athleteRepository = _unitOfWork.GetRepository<Athlete>();
    
        try
        {
            entryRepository.Insert(entry);
            await _unitOfWork.SaveChangesAsync();
        
            var contest = await contestRepository.GetFirstOrDefaultAsync(
                c => c.Id == entry.ContestId,
                null,
                q => q.Include(c => c.Entries),
                TrackingType.Tracking, 
                false,
                false
            );
        
            if (contest == null)
            {
                return Result<Guid>.Fail("Соревнование не найдено.");
            }

            contest.Entries = contest.Entries ?? new List<Entry>();
            contest.Entries.Add(entry);
            
            var user = await userRepository.GetFirstOrDefaultAsync(
                c => c.Id == entry.UserId,
                null,
                q => q.Include(u => u.Entries), 
                TrackingType.Tracking, 
                false, 
                false   
            );
        
            if (user == null)
            {
                return Result<Guid>.Fail("Пользователь не найден.");
            }

            user.Entries = user.Entries ?? new List<Entry>();
            user.Entries.Add(entry);
            
            contestRepository.Update(contest);
            userRepository.Update(user);

            foreach (var athleteId in request.Model.AthletesIds)
            {
                var athlete = await athleteRepository.GetFirstOrDefaultAsync(c => c.Id == athleteId,
                    null,
                    null,
                    TrackingType.Tracking,
                    false,
                    false);
                
                athletes.Add(athlete);
            }
            
            entry.Athletes = (athletes);
            
            entryRepository.Update(entry);
            
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Ok(entry.Id).SetMessage("Заявка успешно создана!");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Fail($"Ошибка при создании заявки: {ex.Message}");
        }
    }
}