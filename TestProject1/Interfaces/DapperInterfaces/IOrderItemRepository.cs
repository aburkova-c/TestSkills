using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItemDTO>> GetAllAsync();
    Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(int orderId);
}
