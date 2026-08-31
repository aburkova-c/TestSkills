using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class AddressRepository : IAddressRepository
{
    private readonly string _connectionString;

    public AddressRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<AddressDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var addresses = await db.QueryAsync<AddressDTO>("select * from Addresses");
        return addresses;
    }
}
