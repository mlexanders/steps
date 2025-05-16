using Steps.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps.Shared.Contracts.ScheduleFile.ViewModel
{
    public class ScheduleFileViewModel : IHaveId
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}
