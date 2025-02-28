using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps.Shared.Contracts.Athletes
{
    public interface IAthleteService
    {
        Task<Result<Guid>> Create(CreateAthleteViewModel createAthleteViewModel);
    }
}
