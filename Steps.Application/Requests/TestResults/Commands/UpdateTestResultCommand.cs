using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.TestResults.Commands;

public record UpdateTestResultCommand(UpdateTestResultViewModel Model) : IRequest<Result<Guid>>;

public class UpdateTestResultCommandHandler : IRequestHandler<UpdateTestResultCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityService _securityService;
    private readonly IMapper _mapper;

    public UpdateTestResultCommandHandler(IUnitOfWork unitOfWork, ISecurityService securityService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _securityService = securityService;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(UpdateTestResultCommand request, CancellationToken cancellationToken)
    {
        var view = request.Model;

        var currentUser = await _securityService.GetCurrentUser() ?? throw new AppAccessDeniedException();

        if (currentUser.Role is not (Role.Organizer or Role.Judge)) throw new AppAccessDeniedException();

        var repository = _unitOfWork.GetRepository<TestResult>();

        var testResultEntity = await repository.FindAsync(view.Id, cancellationToken)
                               ?? throw new StepsBusinessException("Баллы участнику еще не проставлены");

        var updatedTestResult = _mapper.Map(view, testResultEntity);

        updatedTestResult.JudgeId = currentUser.Id; //обновляем судью

        repository.Update(updatedTestResult);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Ok(updatedTestResult.Id).SetMessage("Баллы успешно обновлены");
    }
}