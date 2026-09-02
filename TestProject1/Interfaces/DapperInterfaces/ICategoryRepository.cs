using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDTO>> GetAllAsync();
}
