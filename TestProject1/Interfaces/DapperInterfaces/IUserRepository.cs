using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
}