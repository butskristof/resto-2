using Resto.Common.Exceptions;

namespace Resto.Domain.Common;

public abstract class BaseEntity;

public abstract class BaseEntity<TId> : BaseEntity
{
	public TId Id { get; set; }
}

public abstract class AuditableBaseEntity<TId> : BaseEntity<TId>, IAuditableEntity
{
	#region auditing

	#region private members

	private DateTimeOffset? _modifiedOn;

	#endregion

	#region created

	// public string CreatedBy { get; set; }
	public DateTimeOffset CreatedOn { get; set; }

	#endregion

	#region modified

	// public string LastModifiedBy { get; set; }
	public DateTimeOffset? LastModifiedOn
	{
		get => _modifiedOn;
		set
		{
			if (_modifiedOn.HasValue && value != _modifiedOn)
				throw new DataChangedException();

			_modifiedOn = value;
		}
	}

	public void SetModifiedOnForContext(DateTimeOffset value)
	{
		_modifiedOn = value;
	}

	#endregion

	#endregion
}