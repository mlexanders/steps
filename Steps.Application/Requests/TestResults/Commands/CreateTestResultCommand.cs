using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Domain.Entities.AthletesLists;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.TestResults.Commands;

public record CreateTestResultCommand(CreateTestResultViewModel Model) : IRequest<Result<TestResultViewModel>>;

public class CreateTestResultCommandHandler : IRequestHandler<CreateTestResultCommand, Result<TestResultViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;

    public CreateTestResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ISecurityService securityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
    }

    public async Task<Result<TestResultViewModel>> Handle(CreateTestResultCommand request,
        CancellationToken cancellationToken)
    {
        var model = request.Model;
    
        //TODO: найти блок по спортсмену и мероприятию
        
        var testResult = _mapper.Map<TestResult>(model);

        var currentUser = await _securityService.GetCurrentUser();

        if (currentUser?.Role is not (Role.Organizer or Role.Judge)) throw new AppAccessDeniedException();

        testResult.JudgeId = currentUser.Id;

        var entry = await _unitOfWork.GetRepository<TestResult>().InsertAsync(testResult, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<TestResultViewModel>(entry.Entity);

        return Result<TestResultViewModel>.Ok(viewModel).SetMessage("Баллы сохранены");
    }
}