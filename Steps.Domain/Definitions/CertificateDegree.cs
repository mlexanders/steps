
using System.ComponentModel.DataAnnotations;

namespace Steps.Domain.Definitions;

public enum CertificateDegree
{
     [Display(Name = "Участие")]
     Participant,

     [Display(Name = "Высшая")]
     Higher,
     
     [Display(Name = "I")]
     First,
     
     [Display(Name = "II")]
     Second,
     
     [Display(Name = "III")]
     Third,
     
     [Display(Name = "IV")]
     Fourth
}