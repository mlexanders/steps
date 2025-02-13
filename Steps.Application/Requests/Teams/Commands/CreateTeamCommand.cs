using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Shared.Contracts.Teams.ViewModels;
using Steps.Domain.Entities;
using Steps.Infrastructure.Data;
using Steps.Shared;

namespace Steps.Application.Requests.Teams.Commands;

public record CreateTeamCommand(CreateTeamViewModel Model) : IRequest<Result<Guid>>;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTeamCommandHandler(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _mapper.Map<Team>(request.Model);

        var entity = _unitOfWork.GetRepository<Team>().Insert(team);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Ok(entity.Id).SetMessage("Команда создана");
    }
}