using System.Collections.Concurrent;
using System.Threading.Channels;
using Steps.Application.Events.Base;

namespace Steps.Services.WebApi.Services;

public class ApplicationEventQueue
{
    private readonly ConcurrentDictionary<Type, object> _channels = new();

    public ChannelReader<TEvent> Reader<TEvent>() where TEvent : IApplicationEvent
        => GetOrCreateChannel<TEvent>().Reader;

    public ChannelWriter<TEvent> Writer<TEvent>() where TEvent : IApplicationEvent
        => GetOrCreateChannel<TEvent>().Writer;

    private Channel<TEvent> GetOrCreateChannel<TEvent>() where TEvent : IApplicationEvent
    {
        var type = typeof(TEvent);

        if (_channels.TryGetValue(type, out var existing))
            return (Channel<TEvent>)existing;

        var channel = Channel.CreateUnbounded<TEvent>();

        return (Channel<TEvent>)_channels.GetOrAdd(type, channel);
    }
}