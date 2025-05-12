using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities.GroupBlocks;

namespace Steps.Application.Services;

public class ScheduleFileService
{
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleFileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task GeneratePreScheduleFile(Guid groupBlockId)
    {
        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        
        var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
            predicate: g => g.Id == groupBlockId,
            include: source => source
                .Include(x => x.PreSchedule)
                .ThenInclude(x => x.Athlete)
                .Include(x => x.Contest),
            trackingType: TrackingType.Tracking);
    }
}