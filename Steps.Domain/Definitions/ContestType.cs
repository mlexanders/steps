using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum ContestType
{
    [Display(Name = "Соло")]
    Solo,

    [Display(Name = "Зачет")]
    Test,

    [Display(Name = "Интенсив")]
    Intensive
}