using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.TestResults.Commands;

public record CreateTestResultCommand(CreateTestResultViewModel Model) : IRequest<Result<TestResultViewModel>>;

public class CreateTestResultCommandHandler : IRequestHandler<CreateTestResultCommand, Result<TestResultViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public CreateTestResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<TestResultViewModel>> Handle(CreateTestResultCommand request,
        CancellationToken cancellationToken)
    {
        var model = request.Model;

        var cell = await _unitOfWork.GetRepository<FinalScheduledCell>()
                       .GetFirstOrDefaultAsync(
                           predicate: s => s.AthleteId.Equals(model.AthleteId)
                                           && s.GroupBlock.ContestId.Equals(model.ContestId),
                           include: x => x.Include(c => c.Athlete),
                           trackingType: TrackingType.NoTracking)
                   ?? throw new StepsBusinessException("Участник не найден в финальном блоке");

        var testResult = _mapper.Map<TestResult>(model);

        var currentUser = await _securityService.GetCurrentUser();

        if (currentUser?.Role is not (Role.Organizer or Role.Judge)) throw new AppAccessDeniedException();

        testResult.JudgeId = currentUser.Id;
        testResult.Rating = GetScored(cell, testResult);

        var entry = await _unitOfWork.GetRepository<TestResult>().InsertAsync(testResult, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<TestResultViewModel>(entry.Entity);

        return Result<TestResultViewModel>.Ok(viewModel).SetMessage("Баллы сохранены");
    }

    private static Rating GetScored(FinalScheduledCell cell, TestResult testResult)
    {
        var athlete = cell.Athlete;
        var total = testResult.Scores.Sum();

        return new Rating
        {
            AthleteId = cell.AthleteId,
            TotalScore = total,
            ContestId = testResult.ContestId,
            GroupBlockId = cell.GroupBlockId,
            AgeCategory = athlete.AgeCategory,
            CertificateDegree = GetDegree(athlete, total)
        };
    }

    private static CertificateDegree GetDegree(Athlete athlete, int total)
    {
        var thresholds = new Dictionary<AgeCategory, (int Higher, int First, int Second, int Third, int Fourth)>
        {
            [AgeCategory.Baby] = (42, 40, 39, 34, 32),
            [AgeCategory.YoungerChildren] = (43, 41, 40, 35, 33),
            [AgeCategory.BoysGirls] = (44, 42, 41, 36, 34),
            [AgeCategory.Youth] = (46, 44, 43, 38, 36),
            [AgeCategory.Juniors] = (48, 46, 45, 40, 38) // TODO: Уточнить данные
        };

        if (!thresholds.TryGetValue(athlete.AgeCategory, out var limits))
        {
            throw new ArgumentOutOfRangeException(nameof(athlete.AgeCategory), "Invalid age category.");
        }

        return total >= limits.Higher ? CertificateDegree.Higher :
            total >= limits.First ? CertificateDegree.First :
            total >= limits.Second ? CertificateDegree.Second :
            total >= limits.Third ? CertificateDegree.Third :
            total >= limits.Fourth ? CertificateDegree.Fourth :
            total < limits.Fourth ? CertificateDegree.Participant :
            throw new ArgumentOutOfRangeException(nameof(total), "Invalid total score.");
    }
}