using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Requests.SoloResults.Commands;

public record UpdateSoloResultCommand(UpdateSoloResultViewModel Model) : IRequest<Result<Guid>>;

public class UpdateSoloResultCommandHandler : IRequestHandler<UpdateSoloResultCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSoloResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateSoloResultCommand request, CancellationToken cancellationToken)
    {
        var soloResult = await _unitOfWork.GetRepository<SoloResult>()
            .GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Model.Id,
                trackingType: TrackingType.Tracking,
                cancellationToken: cancellationToken);

        if (soloResult == null)
            return Result<Guid>.Fail("Результат не найден");

        _mapper.Map(request.Model, soloResult);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(soloResult.Id).SetMessage("Результат обновлен");
    }
}
