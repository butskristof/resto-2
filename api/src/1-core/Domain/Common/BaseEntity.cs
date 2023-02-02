using System.ComponentModel.DataAnnotations.Schema;
using Resto.Common.Exceptions;

namespace Resto.Domain.Common;

public abstract class BaseEntity
{
	private readonly List<BaseEvent> _domainEvents = new();
	
	[NotMapped]
	public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

	public void AddDomainEvent(BaseEvent @event)
		=> _domainEvents.Add(@event);
	
	public void RemoveDomainEvent(BaseEvent @event)
		=> _domainEvents.Remove(@event);

	public void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class BaseEntity<TId> : BaseEntity
{
	public TId Id { get; set; }
}

public abstract class AuditableBaseEntity<TId> : BaseEntity<TId>, IAuditableEntity
{
	#region auditing

	#region private members

	private DateTime? _modifiedOn;

	#endregion

	#region created

	// public string CreatedBy { get; set; }
	public DateTime CreatedOn { get; set; }

	#endregion

	#region modified

	// public string LastModifiedBy { get; set; }
	public DateTime? LastModifiedOn
	{
		get => _modifiedOn;
		set
		{
			if (_modifiedOn.HasValue && value != _modifiedOn)
				throw new DataChangedException();

			_modifiedOn = value;
		}
	}

	public void SetModifiedOnForContext(DateTime value)
	{
		_modifiedOn = value;
	}

	#endregion

	#endregion
}