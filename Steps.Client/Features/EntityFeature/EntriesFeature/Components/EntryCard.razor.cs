using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Client.Features.EntityFeature.EntriesFeature.Components;

public partial class EntryCard 
{
    [Inject] protected EntriesManager EntriesManager { get; set; } = null!;
    [Parameter] public EntryViewModel? Model { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var specification = new Specification<Entry>();
        if (Model != null)
        {
            specification.Include(x => x.Include(a => a.Athletes));
        }

        EntriesManager.UseSpecification(specification);

        await base.OnInitializedAsync();
    }
}