using Radzen;
using Steps.Client.Features.Organizer.Dialogs;
using Steps.Shared;
using Steps.Shared.Contracts;
using Steps.Shared.Contracts.Contests;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.Organizer.Services;

public class ContestsManagement
{
    private Page _currentPage;
    private readonly IContestService _contestService;
    private readonly DialogService _dialogService;

    public ContestsManagement(IContestService contestService, DialogService dialogService)
    {
        _contestService = contestService;
        _dialogService = dialogService;
        PageSize = Page.DefaultPageSize;
        _currentPage = new Page(0, PageSize);
        Contests = [];
        TotalCount = 0;
    }

    public bool IsLoading { get; private set; }

    public int TotalCount { get; private set; }

    public int PageSize { get; private set; }

    public List<ContestViewModel> Contests { get; set; }

    public event Action? ContestsChanged;


    public async Task Initialize()
    {
        await Load();
    }

    private async Task Load()
    {
        IsLoading = true;
        try
        {
            var result = await _contestService.GetPaged(_currentPage);
            if (!result.IsSuccess) ShowMessage(result);

            var pagedList = result.Value;
            Contests = pagedList?.Items.ToList() ?? [];
            TotalCount = pagedList?.TotalCount ?? 0;

            OnContestsChanged();
        }
        catch (Exception e)
        {
            HandleException(e);
        }

        IsLoading = false;
    }


    public async Task ChangePage(int? skip, int? take)
    {
        if (skip != null && take != null)
            _currentPage = new Page(true, skip.Value, take.Value);
        await Load();
    }

    private void ChangePageSize(int pageSize)
    {
        PageSize = pageSize > 0 ? pageSize : Page.DefaultPageSize;
    }

    public async Task Create()
    {
        var result = await _dialogService.OpenAsync<ContestDialog>("Создать мероприятие",
            new Dictionary<string, object> { { "IsNew", true } });
        if (result == true) await Load();
    }

    public async Task Update(ContestViewModel contest)
    {
        var result = await _dialogService.OpenAsync<ContestDialog>("Редактировать мероприятие",
            new Dictionary<string, object> { { "Contest", contest }, { "IsNew", false } });
        if (result == true) await Load();
    }

    public async Task Delete(ContestViewModel contest)
    {
        var confirmed = await _dialogService.Confirm("Вы уверены, что хотите удалить это мероприятие?",
            $"Удаление {contest.Name}",
            new ConfirmOptions
            {
                OkButtonText = "Да, удалить",
                CancelButtonText = "Отмена"
            });

        if (confirmed == true)
        {
            await _contestService.Delete(contest.Id);
            await Load();
        }
    }

    protected virtual void OnContestsChanged()
    {
        ContestsChanged?.Invoke();
    }

    private void HandleException(Exception exception)
    {
        Console.WriteLine(exception.Message);
        // cw.Log.Error(exception);
    }

    private void ShowMessage(Result<PaggedListViewModel<ContestViewModel>> result)
    {
        Console.WriteLine(result?.Message);
    }
}