using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Enumerations;

namespace Resto.Application.Common.Contracts.Responses.Orders;

public class OrderDto : IMapFrom<Order>
{
	public Guid Id { get; set; }
	public OrderDiscount Discount { get; set; }
	public DateTime Timestamp { get; set; }
}