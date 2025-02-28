using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps.Shared.Contracts.Athletes.ViewModels
{
    public class CreateAthleteViewModel
    {
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Guid TeamId { get; set; }
    }
}
