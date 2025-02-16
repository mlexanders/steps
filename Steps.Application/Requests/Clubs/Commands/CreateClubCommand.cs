using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Clubs.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.Clubs.Commands;

public record CreateClubCommand(CreateClubViewModel Model) : IRequest<Result<ClubViewModel>>;

public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, Result<ClubViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;
    private readonly IMapper _mapper;

    public CreateClubCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
        _mapper = mapper;
    }

    public async Task<Result<ClubViewModel>> Handle(CreateClubCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (currentUser.Role is not Role.Organizer && !model.OwnerId.Equals(currentUser.Id))
        {
            throw new AppAccessDeniedException();
        }

        var repository = _unitOfWork.GetRepository<Club>();

        var clubIsExist = await repository
            .ExistsAsync(c => c.Name.Equals(model.Name),
                cancellationToken: cancellationToken);
        if (clubIsExist) throw new StepsBusinessException("Клуб с таким названием уже существует");

        var club = _mapper.Map<Club>(model);

        var entity = repository.Insert(club);
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<ClubViewModel>(entity);

        return Result<ClubViewModel>.Ok(viewModel).SetMessage("Клуб успешно создан");
    }
}