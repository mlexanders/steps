using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Clubs.Commands;

public record DeleteClubCommand(Guid Id) : IRequest<Result>;

public class DeleteClubCommandHandler : IRequestHandler<DeleteClubCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;
    private readonly IMapper _mapper;

    public DeleteClubCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
        _mapper = mapper;
    }

    public async Task<Result> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

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