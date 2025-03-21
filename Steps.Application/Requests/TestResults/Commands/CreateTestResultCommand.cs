using AutoMapper;
using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;
using Steps.Shared.Exceptions;

namespace Steps.Application.Requests.TestResults.Commands;

public record CreateTestResultCommand(CreateTestResultViewModel Model) : IRequest<Result<TestResultViewModel>>;

public class CreateTestResultCommandHandler : IRequestHandler<CreateTestResultCommand, Result<TestResultViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISecurityService _securityService;
    private readonly IHubContext<TestResultHub> _hubContext;

    public CreateTestResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ISecurityService securityService, IHubContext<TestResultHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _securityService = securityService;
        _hubContext = hubContext;
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

        await SendTestResultWithRetry(viewModel, model.AthleteId);

        return Result<TestResultViewModel>.Ok(viewModel).SetMessage("Баллы сохранены");
    }

    private async Task SendTestResultWithRetry(TestResultViewModel viewModel, Guid athleteId, int retries = 3)
    {
        for (int i = 0; i < retries; i++)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("ReceiveTestResult", viewModel);

                //await _hubContext.Clients.All.SendAsync("RemoveAthlete", athleteId);
                return; // Успех, выходим из метода
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки в SignalR: {ex.Message}");

                if (i < retries - 1)
                {
                    Console.WriteLine("Повторная попытка через 2 секунды...");
                    await Task.Delay(2000); // Ждём 2 секунды перед повторной попыткой
                }
                else
                {
                    Console.WriteLine("Ошибка после всех попыток. Данные не отправлены.");
                }
            }
        }
    }
}