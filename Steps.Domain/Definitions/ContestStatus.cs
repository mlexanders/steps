using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum ContestStatus
{
    [Display(Name = "Открыт приём заявок")]
    Open,
    
    [Display(Name = "Закрыт приём заявок")]
    Closed,
    
    [Display(Name = "Мероприятие завершено")]
    Finished
}