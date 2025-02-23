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

    public ContestsManagement(IContestService contestService)
    {
        _contestService = contestService;
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
            // if (!result.IsSuccess) ShowMessage(result.Message);

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

    public async Task<Result<Guid>> Create(CreateContestViewModel contest)
    {
        try
        {
            return await _contestService.Create(contest);
        }
        catch (Exception e)
        {
            return Result<Guid>.Fail(e.Message);
        }
    }

    public async Task<Result<Guid>> Update(UpdateContestViewModel contest)
    {
        try
        {
            return await _contestService.Update(contest);
        }
        catch (Exception e)
        {
            return Result<Guid>.Fail(e.Message);
        }
    }

    public async Task<Result> Delete(ContestViewModel contest)
    {
        try
        {
            return await _contestService.Delete(contest.Id);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
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
}