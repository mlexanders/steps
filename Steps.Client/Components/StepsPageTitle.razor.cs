using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Steps.Client.Components;

public partial class StepsPageTitle
{
    [Required] [Parameter] public RenderFragment? ChildContent { get; set; }
}