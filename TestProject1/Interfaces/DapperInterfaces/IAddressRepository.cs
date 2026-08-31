using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface IAddressRepository
{
    Task<IEnumerable<AddressDTO>> GetAllAsync();
}
