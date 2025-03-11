using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared;
using Steps.Application.Helpers;
using Steps.Application.Requests.Athletes.Queries;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Utils;
using Steps.Shared.Contracts;

namespace Steps.Application.Requests.Users.Queries
{
    public record GetJudgesQuery(Page Page) : IRequest<Result<PaggedListViewModel<UserViewModel>>>;

    public class GetJudgesQueryHandler : IRequestHandler<GetJudgesQuery, Result<PaggedListViewModel<UserViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetJudgesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaggedListViewModel<UserViewModel>>> Handle(GetJudgesQuery request,
            CancellationToken cancellationToken)
        {
            var page = request.Page;

            var repository = _unitOfWork.GetRepository<User>();

            var contests = await repository.GetPagedListAsync(
                selector: contest => _mapper.Map<UserViewModel>(contest),
                predicate: u => u.Role == Role.Judge,
                pageIndex: page.PageIndex,
                pageSize: page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);

            return Result<PaggedListViewModel<UserViewModel>>.Ok(contests.GetView());
        }
    }
}
