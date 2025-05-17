using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum AgeCategory
{
    [Display(Name = "Беби 3-4")]
    Baby,            // 3-4 года (2021 и младше)

    [Display(Name = "Мальчики, девочки 5-7")]
    YoungerChildren, // 5-7 лет (2018-2020)
    
    [Display(Name = "Мальчики, девочки 8-11")]
    BoysGirls,       // 8-11 лет (2014-2017)
    
    [Display(Name = "Юноши девушки 12-14")]
    Youth,          // 12-14 лет (2011-2013)

    [Display(Name = "Юниоры, юниорки 15-18")]
    Juniors         // 15-18 лет (2007-2010)
}