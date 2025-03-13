using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Entries.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Entries.Commands;

public record AcceptEntryCommand(EntryViewModel Model) : IRequest<Result>;

public class AcceptEntryCommandHandler : IRequestHandler<AcceptEntryCommand, Result>, IRequireAuthorization
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
        var model = request.Model;
        var entry = _mapper.Map<Entry>(model);

        entry.IsSuccess = true;
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage("Заявка одобрена");
    }

    public Task<bool> CanAccess(IUser user)
    {
        return Task.FromResult(user.Role is Role.Organizer);
    }
}