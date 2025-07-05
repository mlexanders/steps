using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Clubs.Commands;

public record DeleteClubCommand(Guid ClubId) : IRequest<Result>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user) => Task.FromResult(user.Role is Role.Organizer);
}

public class DeleteClubCommandHandler : IRequestHandler<DeleteClubCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;

    public DeleteClubCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
    }

    public async Task<Result> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
    {
        var id = request.ClubId;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        var repository = _unitOfWork.GetRepository<Club>();
        var club = await repository.FindAsync(id) ?? throw new AppNotFoundException("Клуб не найден");

        if (currentUser.Role is not Role.Organizer && !club.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        repository.Delete(club); //TODO: Restricted на удаление - команд 
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage($"Клуб {club.Name} удален");
    }
}