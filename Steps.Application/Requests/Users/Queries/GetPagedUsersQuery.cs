using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Application.Interfaces;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Users.Queries;

public record GetPagedUsersQuery(Page Page, Specification<User>? Specification) : 
    SpecificationRequest<User>(Specification), IRequest<Result<PaggedListViewModel<UserViewModel>>>;

public class GetPagedUsersHandler
    : IRequestHandler<GetPagedUsersQuery,
        Result<PaggedListViewModel<UserViewModel>>>, IRequireAuthorization
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedUsersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }

    public async Task<Result<PaggedListViewModel<UserViewModel>>> Handle(GetPagedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var views = await _unitOfWork.GetRepository<User>()
            .GetPagedListAsync(
                selector: (user) => _mapper.Map<UserViewModel>(user),
                predicate: request.Predicate,
                include: request.Includes,
                pageIndex: request.Page.PageIndex,
                pageSize: request.Page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);

        var result = Result<PaggedListViewModel<UserViewModel>>.Ok(views.GetView());
        return result;
    }

}