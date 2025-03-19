using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Helpers;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Athletes.Queries
{
    public record GetPagedAthletesQuery(Page Page, Specification<Athlete>? Specification)
        : SpecificationRequest<Athlete>(Specification), IRequest<Result<PaggedListViewModel<AthleteViewModel>>>;

    public class GetPagedAthletesQueryHandler : IRequestHandler<GetPagedAthletesQuery, Result<PaggedListViewModel<AthleteViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetPagedAthletesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaggedListViewModel<AthleteViewModel>>> Handle(GetPagedAthletesQuery request,
            CancellationToken cancellationToken)
        {
            var page = request.Page;

            var repository = _unitOfWork.GetRepository<Athlete>();

            var contests = await repository.GetPagedListAsync(
                selector: contest => _mapper.Map<AthleteViewModel>(contest),
                orderBy: contest => contest.OrderBy(c => c.BirthDate),
                predicate: request.Predicate,
                include: request.Includes,
                pageIndex: page.PageIndex,
                pageSize: page.PageSize,
                cancellationToken: cancellationToken,
                trackingType: TrackingType.NoTracking);

            return Result<PaggedListViewModel<AthleteViewModel>>.Ok(contests.GetView());
        }
    }
}
