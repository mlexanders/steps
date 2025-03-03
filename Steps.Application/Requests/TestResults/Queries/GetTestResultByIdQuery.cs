using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Requests.TestResults.Queries;

public record GetTestResultByIdQuery(Guid Id) : IRequest<Result<TestResultViewModel>>;

public class GetTestResultByIdQueryHandler : IRequestHandler<GetTestResultByIdQuery, Result<TestResultViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTestResultByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TestResultViewModel>> Handle(GetTestResultByIdQuery request,
        CancellationToken cancellationToken)
    {
        var testResult = await _unitOfWork.GetRepository<TestResult>()
            .GetFirstOrDefaultAsync(
                predicate: t => t.Id.Equals(request.Id),
                trackingType: TrackingType.NoTracking);

        if (testResult == null) return Result<TestResultViewModel>.Fail("Баллы не найдены");

        var viewModel = _mapper.Map<TestResultViewModel>(testResult);

        return Result<TestResultViewModel>.Ok(viewModel);
    }
}