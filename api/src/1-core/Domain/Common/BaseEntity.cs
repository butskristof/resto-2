using Resto.Common.Exceptions;

namespace Resto.Domain.Common;

public abstract class BaseEntity : IAuditableEntity
{
	public Guid Id { get; set; }

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
			// if (_modifiedOn.HasValue && value != _modifiedOn)
			if (value != _modifiedOn)
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