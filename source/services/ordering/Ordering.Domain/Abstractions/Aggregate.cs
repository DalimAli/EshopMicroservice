﻿namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _events.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {

        _events.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvent()
    {
        var dequeueEvents = _events.ToArray();
        _events.Clear();

        return dequeueEvents;
    }
}
