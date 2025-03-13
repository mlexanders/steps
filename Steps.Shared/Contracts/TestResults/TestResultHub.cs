using Microsoft.AspNetCore.SignalR;
using Steps.Shared.Contracts.TestResults.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps.Shared.Contracts.TestResults
{
    public class TestResultHub : Hub
    {
        public async Task SendTestResult(TestResultViewModel testResult)
        {
            await Clients.All.SendAsync("ReceiveTestResult", testResult);
        }
    }
}
