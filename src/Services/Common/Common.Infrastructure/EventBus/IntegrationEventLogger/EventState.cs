namespace Common.Infrastructure.EventBus.IntegrationEventLogger;

public class EventState : Enumeration
{
    public static EventState NotPublished = new(1, nameof(NotPublished).ToLowerInvariant());
    public static EventState InProgress = new(2, nameof(InProgress).ToLowerInvariant());
    public static EventState Published = new(3, nameof(Published).ToLowerInvariant());
    public static EventState PublishedFailed = new(4, nameof(PublishedFailed).ToLowerInvariant());

    public EventState(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<EventState> List() =>
        new[] { NotPublished, InProgress, Published, PublishedFailed };

    public static EventState FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new Exception($"Possible values for EventState: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static EventState From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new Exception($"Possible values for EventState: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}