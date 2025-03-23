using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvents(IDomainEvent domainEvents)
    {
        _domainEvents.Add(domainEvents);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequedDomainEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequedDomainEvents;
    }
}