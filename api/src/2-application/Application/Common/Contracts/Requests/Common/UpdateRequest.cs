namespace Resto.Application.Common.Contracts.Requests.Common;

public abstract class UpdateRequest<TId>
{
	public TId Id { get; set; }
	// TODO remove
	public DateTimeOffset? LastModifiedOn { get; set; }
}