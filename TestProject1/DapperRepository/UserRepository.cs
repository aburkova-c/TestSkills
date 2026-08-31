using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var users = await db.QueryAsync<UserDTO>("select * from Users");
        return users;
    }
}