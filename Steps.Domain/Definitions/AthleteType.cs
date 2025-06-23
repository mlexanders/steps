using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum AthleteType
{
    [Display(Name = "Чир")]
    Cheer,

    [Display(Name = "Фристайл")]
    CheerFreestyle
}