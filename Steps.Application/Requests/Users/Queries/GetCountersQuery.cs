using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Users.Queries
{
    public record GetCountersQuery(Page Page) : IRequest<Result<PaggedListViewModel<UserViewModel>>>;

    public class GetCountersQueryHandler : IRequestHandler<GetCountersQuery, Result<PaggedListViewModel<UserViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetCountersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaggedListViewModel<UserViewModel>>> Handle(GetCountersQuery request,
            CancellationToken cancellationToken)
        {
            var page = request.Page;

            var repository = _unitOfWork.GetRepository<User>();

            var contests = await repository.GetPagedListAsync(
                selector: contest => _mapper.Map<UserViewModel>(contest),
                predicate: u => u.Role == Domain.Definitions.Role.Counter,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);

            return Result<PaggedListViewModel<UserViewModel>>.Ok(contests.GetView());
        }
    }
}
