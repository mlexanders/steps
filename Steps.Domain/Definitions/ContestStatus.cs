using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum ContestStatus
{
    [Display(Name = "Открыт набор заявок")]
    Open,
    
    [Display(Name = "Закрыт набор заявок")]
    Closed,
    
    [Display(Name = "Мероприятие завершено")]
    Finished
}