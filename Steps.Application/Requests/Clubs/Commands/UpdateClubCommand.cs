using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Clubs.Commands;

public record UpdateClubCommand(UpdateClubViewModel Model) : IRequest<Result<Guid>>, IRequireAuthorization
{
    public Task<bool> CanAccess(IUser user) =>
        Task.FromResult(user.Role is Role.Organizer || Model.OwnerId.Equals(user.Id));
}

public class UpdateClubCommandHandler : IRequestHandler<UpdateClubCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;
    private readonly IMapper _mapper;

    public UpdateClubCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (currentUser.Role is not Role.Organizer && !model.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        var repository = _unitOfWork.GetRepository<Club>();

        var clubEntity = await repository.FindAsync(model.Id, cancellationToken)
                         ?? throw new StepsBusinessException("Не найден клуб");

        var updatedClub = _mapper.Map(model, clubEntity);

        repository.Update(updatedClub);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Ok(updatedClub.Id).SetMessage("Клуб успешно обновлен");
    }
}