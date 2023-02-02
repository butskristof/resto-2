namespace Resto.Application.Common.Contracts.Requests.Common;

public abstract class UpdateRequest<TId>
{
	public TId Id { get; set; }
	public DateTime? LastModifiedOn { get; set; }
}