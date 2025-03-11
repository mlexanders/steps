using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Application.Interfaces.Base;
using Steps.Application.Requests.Teams.Queries;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Entries.ViewModels;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Entries.Queries;

public record class GetPagedEntriesQuery(Page Page, Specification<Entry>? Specification)
    : SpecificationRequest<Entry>(Specification), IRequest<Result<PaggedListViewModel<EntryViewModel>>>;

public class GetPagedEntriesQueryHandler
    : IRequestHandler<GetPagedEntriesQuery,
        Result<PaggedListViewModel<EntryViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public GetPagedEntriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<PaggedListViewModel<EntryViewModel>>> Handle(GetPagedEntriesQuery request, CancellationToken cancellationToken)
    {
        var user = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();
        
        if (user.Role != Role.Organizer) request.AddPredicate(t => t.CreatorId.Equals(user.Id)); // Только заявки пользователя
        
        var views = await _unitOfWork.GetRepository<Entry>()
            .GetPagedListAsync(
                selector: entry => new EntryViewModel
                {
                    Id = entry.Id,
                    Number = entry.Number,
                    IsSuccess = entry.IsSuccess,
                    ContestId = entry.ContestId,
                    ContestName = entry.Contest.Name,
                    Athletes = entry.Athletes.Select(a => new Athlete() 
                    { 
                        Id = a.Id, 
                        FullName = a.FullName
                    }).ToList()
                },
                predicate: request.Predicate,
                include: request.Includes,
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);
        
        var result = Result<PaggedListViewModel<EntryViewModel>>.Ok(views.GetView());

        return result;
    }
}