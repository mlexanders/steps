// using Calabonga.UnitOfWork;
// using Microsoft.EntityFrameworkCore;
// using Steps.Domain.Definitions;
// using Steps.Domain.Entities;
// using Steps.Domain.Entities.GroupBlocks;
// using Steps.Shared.Exceptions;
//
// namespace Steps.Application;
//
// public class GroupBlockService
// {
//     private const int DefaultJudgesCount = 2;
//     private static readonly TimeSpan GroupBlockInterval = TimeSpan.FromMinutes(30);
//     private static readonly TimeSpan AthleteExitInterval = TimeSpan.FromMinutes(2);
//
//     private readonly IUnitOfWork _unitOfWork;
//
//     public GroupBlockService(IUnitOfWork unitOfWork)
//     {
//         _unitOfWork = unitOfWork;
//     }
//
//     /// <summary>
//     /// Возвращает список групповых блоков для соревнования.
//     /// </summary>
//     public async Task<List<GroupBlock>> GetGroupBlocks(Contest contest)
//     {
//         //TODO: переделать под ViewModels
//
//         var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
//
//         var groupBlocks = await groupBlockRepository.GetAllAsync(
//             predicate: gb => gb.ContestId.Equals(contest.Id),
//             include: gb => gb.Include(a => a.AthleteSubGroup),
//             trackingType: TrackingType.NoTracking);
//
//         return groupBlocks?.ToList() ?? new List<GroupBlock>();
//     }
//
//     // private IEnumerable<AthleteGroupBlockViewModel> GetViewAthletes(List<AthleteSubGroup> groupBlockPreSubGroups)
//     // {
//     //     foreach (var subGroup in groupBlockPreSubGroups)
//     //     {
//     //         var athletes = subGroup.AthleteBlocks.Select(a => a.Athlete);
//     //         foreach (var athlete in athletes)
//     //         {
//     //             yield return new AthleteGroupBlockViewModel
//     //             {
//     //                 GroupBlockId = subGroup.GroupBlockId,
//     //                 ExitTime = subGroup.ExitTime,
//     //                 Athlete = new()
//     //                 {
//     //                     Id = athlete.Id,
//     //                     FullName = athlete.FullName,
//     //                     BirthDate = athlete.BirthDate,
//     //                     AthleteType = athlete.AthleteType,
//     //                     Degree = athlete.Degree,
//     //                     TeamId = athlete.TeamId,
//     //                 }
//     //             };
//     //         }
//     //     }
//     // }
//
//     /// <summary>
//     /// Помечает спортсмена как подтвержденного в заданном блоке.
//     /// </summary>
//     public async Task MarkAthlete(GroupBlock groupBlock, Guid athleteId)
//     {
//         var athleteBlockRepository = _unitOfWork.GetRepository<AthleteSubGroup>();
//
//         var athleteBlock = await athleteBlockRepository.GetFirstOrDefaultAsync(
//                                predicate: b => b.GroupBlockId.Equals(groupBlock.Id) && b.AthleteId.Equals(athleteId),
//                                trackingType: TrackingType.Tracking)
//                            ?? throw new StepsBusinessException("Спортсмен в этом предварительном блоке не найден.");
//
//         athleteBlock.IsConfirmed = true;
//         await _unitOfWork.SaveChangesAsync();
//     }
//
//     public async Task GenerateFinalGroupBlocks(Guid groupBlockId)
//     {
//         var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
//         var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
//             predicate: gb => gb.Id.Equals(groupBlockId),
//             include: gb => gb
//                 .Include(a => a.Contest)
//                 .Include(a => a.AthleteSubGroup),
//                                  // .ThenInclude(ah => ah),
//                              trackingType: TrackingType.Tracking)
//                          ?? throw new StepsBusinessException("Предварительный блок не найден");
//
//         ValidateContestStatus(groupBlock.Contest);
//         
//         var isExistFinalGroups = groupBlock.AthleteSubGroup.Any(s => s.FinalSubGroupId != null);
//         if (isExistFinalGroups)
//             throw new StepsBusinessException("Финальный блок для выбранного предварительного блока уже сформирован");
//
//         var athleteSubGroups = groupBlock.AthleteSubGroup
//             .Where(p => p.IsConfirmed)
//             .ToList();
//
//         if (athleteSubGroups is null || athleteSubGroups.Count == 0)
//             throw new StepsBusinessException("Предварительный групповой блок пуст");
//
//         // создание подгруппы
//         // for (var index = 0; index < athleteSubGroups.Count; index++)
//         // {
//         //     var athleteSubGroup = athleteSubGroups[index];
//         //     var exitTime = groupBlock.StartTime.Add(AthleteExitInterval * (index + 1));
//
//         //     var finalSubGroup = new FinalSubGroup()
//         //     {
//         //         GroupBlock = groupBlock,
//         //         ExitTime = exitTime.ToUniversalTime(),
//         //         AthleteBlocks = []
//         //     };
//         //
//         //     athleteSubGroup.FinalSubGroup = finalSubGroup;
//         //     groupBlock.FinalSubGroups.Add(finalSubGroup);
//         //
//         //     var sequenceNumber = 1;
//         //     foreach (var athleteId in athleteSubGroup)
//         //     {
//         //         finalSubGroup.AthleteBlocks.Add(new FinalAthleteSubGroup()
//         //         {
//         //             SequenceNumber = sequenceNumber,
//         //             AthleteId = athleteId.AthleteId,
//         //             SubGroup = finalSubGroup,
//         //         });
//         //         sequenceNumber++;
//         //     }
//         // }
//         //
//         // groupBlockRepository.Update(groupBlock);
//         // await _unitOfWork.SaveChangesAsync();
//     }
//
//
//     /// <summary>
//     /// Генерирует ПРЕДВАРИТЕЛЬНЫЕ групповые блоки для соревнования.
//     /// </summary>
//     public async Task GeneratePreGroupBlocks(Contest contest, int athletesPerGroup)
//     {
//         ValidateContestStatus(contest);
//
//         var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
//         var entryRepository = _unitOfWork.GetRepository<Entry>();
//
//         var groupBlocksExist = await groupBlockRepository.ExistsAsync(gb => gb.ContestId.Equals(contest.Id));
//         if (groupBlocksExist)
//             throw new StepsBusinessException("Список уже сформирован.");
//
//         var athleteEntries = await entryRepository.GetAllAsync(
//             predicate: entry => entry.ContestId.Equals(contest.Id) && entry.IsSuccess,
//             selector: entry => entry.Athletes.Select(a => a.Id).Distinct(),
//             trackingType: TrackingType.NoTracking);
//
//         var athleteIds = athleteEntries.SelectMany(a => a).Distinct().ToList();
//         var sortedAthleteIds = GetSortedAthletes(athleteIds).ToList();
//
//         var judgeCount = contest.Judges?.Count ?? DefaultJudgesCount;
//         if (judgeCount == 0) judgeCount = DefaultJudgesCount;
//
//         var groupedAthletes = SplitIntoBatches(sortedAthleteIds, athletesPerGroup);
//         var groupBlocks = new List<GroupBlock>();
//
//         foreach (var athleteBatch in groupedAthletes)
//         {
//             var startTime = groupBlocks.Count > 0
//                 ? groupBlocks.Last().EndTime.Add(GroupBlockInterval)
//                 : contest.StartDate;
//
//             var groupBlock = new GroupBlock
//             {
//                 ContestId = contest.Id,
//                 StartTime = startTime.ToUniversalTime()
//             };
//             groupBlocks.Add(groupBlock);
//
//             var athletesInSubGroups = SplitIntoBatches(athleteBatch, judgeCount);
//
//             // создание подгруппы
//             var sequenceNumber = 1;
//             for (var index = 0; index < athletesInSubGroups.Count; index++)
//             {
//                 var exitTime = groupBlock.StartTime.Add(AthleteExitInterval * (index + 1));
//                 var athletesSubGroup = athletesInSubGroups[index];
//
//                 foreach (var athleteId in athletesSubGroup)
//                 {
//                     var athleteSubGroup = new AthleteSubGroup
//                     {
//                         // GroupBlockId = default,
//                         SequenceNumber = sequenceNumber,
//                         GroupBlock = groupBlock,
//                         ExitTime = exitTime,
//                         AthleteId = athleteId,
//                     };
//                     sequenceNumber++;
//                     groupBlock.AthleteSubGroup.Add(athleteSubGroup);
//                 }
//               
//             }
//
//             groupBlock.EndTime = groupBlock.AthleteSubGroup.Last().ExitTime;
//         }
//
//         await groupBlockRepository.InsertAsync(groupBlocks);
//         await _unitOfWork.SaveChangesAsync();
//     }
//
//     /// <summary>
//     /// Валидация состояния соревнования.
//     /// </summary>
//     /// <param name="contest"></param>
//     /// <exception cref="StepsBusinessException"></exception>
//     private static void ValidateContestStatus(Contest contest)
//     {
//         if (contest.Status is ContestStatus.Open)
//             throw new StepsBusinessException("Сбор заявок не закрыт.");
//         if (contest.Status is ContestStatus.Finished)
//             throw new StepsBusinessException("Мероприятие уже завершено.");
//     }
//
//
//     /// <summary>
//     /// Разделяет список на подгруппы заданного размера.
//     /// </summary>
//     private static List<List<T>> SplitIntoBatches<T>(List<T> items, int batchSize)
//     {
//         return items
//             .Select((item, index) => new { item, index })
//             .GroupBy(x => x.index / batchSize)
//             .Select(g => g.Select(x => x.item).ToList())
//             .ToList();
//     }
//
//     /// <summary>
//     /// Сортирует список спортсменов (при необходимости можно добавить логику сортировки).
//     /// </summary>
//     private static IEnumerable<Guid> GetSortedAthletes(IEnumerable<Guid> athleteIds)
//     {
//         return athleteIds; // TODO: 
//     }
// }