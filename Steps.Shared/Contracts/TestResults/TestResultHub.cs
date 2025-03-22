using Microsoft.AspNetCore.SignalR;
using Steps.Shared.Contracts.TestResults.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steps.Shared.Contracts.Athletes;

namespace Steps.Shared.Contracts.TestResults
{
    public class TestResultHub : Hub
    {
        
        public async Task SendTestResult(TestResultViewModel result)
        {
            await Clients.All.SendAsync("ReceiveTestResult", result);
        }

        public async Task RemoveAthlete(Guid athleteId)
        {
            await Clients.All.SendAsync("RemoveAthlete", athleteId);
        }
        
        public async Task NotifyRatingsUpdated()
        {
            await Clients.All.SendAsync("RatingsUpdated");
        }
    }
}
