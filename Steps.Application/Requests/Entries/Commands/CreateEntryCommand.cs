using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Entries.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Entries.Commands;

public record CreateEntryCommand(CreateEntryViewModel Model) : IRequest<Result<Guid>>;

public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public CreateEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<Guid>> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = request.Model;
        var creator = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (entry?.AthletesIds == null || entry.AthletesIds.Count <= 0)
            throw new StepsBusinessException("Список участников пуст");

        var athletes = await _unitOfWork.GetRepository<Athlete>().GetAllAsync(
                           predicate: a => entry.AthletesIds.Contains(a.Id),
                           trackingType: TrackingType.Tracking)
                       ?? throw new StepsBusinessException("Неверный список участников");

        var entity = _mapper.Map<Entry>(entry);
        entity.Athletes = athletes.ToList();
        entity.CreatorId = creator.Id;

        var entityEntry = await _unitOfWork.GetRepository<Entry>().InsertAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Ok(entityEntry.Entity.Id).SetMessage("Заявка подана");
    }
}