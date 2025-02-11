namespace Resto.Application.Common.Contracts.Requests.Common;

internal interface IUpdateRequest<TId>
{
	public TId Id { get; init; }
	public DateTimeOffset? LastModifiedOn { get; init; }
};