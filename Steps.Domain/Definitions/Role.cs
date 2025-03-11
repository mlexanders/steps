using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum Role
{
    [Display(Name = "Новый пользователь")]
    Undefined,
    
    [Display(Name="Организатор")]
    Organizer,
    
    [Display(Name="Счетчик")]
    Counter,
    
    [Display(Name="Судья")]
    Judge,
    
    [Display(Name="Пользователь")]
    User
}