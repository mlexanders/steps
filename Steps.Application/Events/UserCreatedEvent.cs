using Steps.Application.Events.Base;
using Steps.Domain.Base;

namespace Steps.Application.Events;

public record UserCreatedEvent(IUser Entity, string Password) : IApplicationEvent;

public class UserCreatedEventHandler : IApplicationEventHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent applicationEvent, CancellationToken cancellationToken)
    {
        //TODO:
        _logger.LogInformation($"User created: {applicationEvent.Entity.Login} \n {applicationEvent.Password}");
        return Task.CompletedTask;
    }
}
