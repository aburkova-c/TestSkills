using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDTO>> GetAllAsync();
    Task<OrderDTO?> GetByIdAsync(int id);
}
