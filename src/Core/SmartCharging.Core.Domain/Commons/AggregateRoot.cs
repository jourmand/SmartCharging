namespace SmartCharging.Core.Domain.Commons;

public abstract class AggregateRoot : IInternalEventHandler
{
    public Guid Id { get; protected set; }

    private readonly List<object> _changes;

    protected AggregateRoot() => _changes = [];

    public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

    public void ClearChanges() => _changes.Clear();

    protected abstract void When(object @event);

    protected void Apply(object @event)
    {
        When(@event);
        EnsureValidState();
    }

    protected abstract void EnsureValidState();
    protected void ApplyToEntity(IInternalEventHandler entity, object @event) =>
        entity?.Handle(@event);

    public void Handle(object @event) => When(@event);
}
