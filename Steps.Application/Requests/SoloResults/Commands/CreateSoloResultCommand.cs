using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Application.Events.Base;
using Steps.Application.Events.SoloResults;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.SoloResults.Commands;

public record CreateSoloResultCommand(CreateSoloResultViewModel Model) : IRequest<Result<SoloResultViewModel>>;

public class CreateSoloResultCommandHandler : IRequestHandler<CreateSoloResultCommand, Result<SoloResultViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IApplicationEventPublisher _eventPublisher;
    private readonly ISecurityService _securityService;

    public CreateSoloResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ISecurityService securityService, IApplicationEventPublisher eventPublisher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<SoloResultViewModel>> Handle(CreateSoloResultCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var model = request.Model;

            var cell = await _unitOfWork.GetRepository<FinalScheduledCell>()
                           .GetFirstOrDefaultAsync(
                               predicate: s => s.AthleteId.Equals(model.AthleteId)
                                               && s.GroupBlock.ContestId.Equals(model.ContestId),
                               include: x => x.Include(c => c.Athlete),
                               trackingType: TrackingType.Tracking)
                       ?? throw new StepsBusinessException("Участник не найден в финальном блоке");

            var soloResult = _mapper.Map<SoloResult>(model);

            var currentUser = await _securityService.GetCurrentUser();

            if (currentUser?.Role is not (Role.Organizer or Role.Judge)) throw new AppAccessDeniedException();

            soloResult.JudgeId = currentUser.Id;
            soloResult.Rating = GetScored(cell, soloResult);

            var entry = await _unitOfWork
                .GetRepository<SoloResult>()
                .InsertAsync(soloResult, cancellationToken);

            cell.HasScore = true;

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);

            var viewModel = _mapper.Map<SoloResultViewModel>(entry.Entity);

            await _eventPublisher.PublishAsync(new SoloResultCreatedEvent(viewModel), cancellationToken);
            return Result<SoloResultViewModel>.Ok(viewModel).SetMessage("Баллы и комментарии сохранены");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result<SoloResultViewModel>.Fail(e.Message);
        }
    }

    private static Rating GetScored(FinalScheduledCell cell, SoloResult soloResult)
    {
        var athlete = cell.Athlete;
        var total = soloResult.Scores.Sum();

        return new Rating
        {
            AthleteId = cell.AthleteId,
            TotalScore = total,
            ContestId = soloResult.ContestId,
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
