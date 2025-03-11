using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.TestResults.Commands;

public record DeleteTestResultCommand(Guid TeamId) : IRequest<Result>;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTestResultCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;

    public DeleteTeamCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
    }


    public async Task<Result> Handle(DeleteTestResultCommand request, CancellationToken cancellationToken)
    {
        var id = request.TeamId;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();
        if (currentUser.Role is not Role.Organizer)
        {
            throw new AppAccessDeniedException();
        }
        
        var repository = _unitOfWork.GetRepository<TestResult>();
        var testResult = await repository.FindAsync(id) ?? throw new AppNotFoundException("Оценки не найдены");

        repository.Delete(testResult);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok().SetMessage($"Оценки участника {testResult.Athlete.FullName} удалены");
    }
}