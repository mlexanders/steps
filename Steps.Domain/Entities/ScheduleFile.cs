using Steps.Domain.Base;

namespace Steps.Domain.Entities;

public class ScheduleFile : Entity
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}