namespace Resto.Domain.Common;

public interface IAuditableEntity
{
	// string CreatedBy { get; set; }
	DateTimeOffset CreatedOn { get; set; }

	// string LastModifiedBy { get; set; }
	DateTimeOffset? LastModifiedOn { get; set; }
	void SetModifiedOnForContext(DateTimeOffset value);
}