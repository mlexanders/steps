namespace Steps.Domain.Base;

public class Entity : IHaveId
{
    public Guid Id { get; set; }
}