using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Teams.Commands;

public record UpdateTeamCommand(UpdateTeamViewModel Model) : IRequest<Result>;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;
    private readonly IMapper _mapper;

    public UpdateTeamCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (currentUser.Role is not Role.Organizer && !model.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        var repository = _unitOfWork.GetRepository<Team>();

        var teamEntity = await repository.FindAsync(model.Id, cancellationToken)
                         ?? throw new StepsBusinessException("Команда не найдена");

        var updatedTeam = _mapper.Map(model, teamEntity);

        repository.Update(updatedTeam);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage("Клуб успешно обновлен");
    }
}