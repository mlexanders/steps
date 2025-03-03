using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;
using Steps.Shared;

namespace Steps.Application.Requests.Entries.Commands;

public record AcceptEntryCommand(Guid ModelId) : IRequest<Result>;

public class AcceptEntryCommandHandler : IRequestHandler<AcceptEntryCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AcceptEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(AcceptEntryCommand request, CancellationToken cancellationToken)
    {
        var modelId = request.ModelId;

        var entryRepository = _unitOfWork.GetRepository<Entry>();

        try
        {
            var entry = await entryRepository.GetFirstOrDefaultAsync(e => e.Id == modelId,
                null,
                null,
                TrackingType.Tracking,
                false,
                false);

            entry.IsSuccess = true;

            entryRepository.Update(entry);

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Ok(entry.Id).SetMessage("Заявка успешно создана!");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Fail($"Ошибка при создании заявки: {ex.Message}");
        }
    }
}