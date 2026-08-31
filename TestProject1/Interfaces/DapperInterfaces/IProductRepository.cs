using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDTO>> GetAllAsync();
    Task<ProductDTO?> GetByIdAsync(int id);
}
