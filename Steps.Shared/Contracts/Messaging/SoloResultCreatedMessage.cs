using Steps.Shared.Contracts.Messaging.Notifications;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Shared.Contracts.Messaging;

public class SoloResultCreatedMessage(SoloResultViewModel model) : MessageContract<SoloResultCreatedMessage>
{
    public SoloResultViewModel Model { get; init; } = model;
}
