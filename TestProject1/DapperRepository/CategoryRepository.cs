using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var categories = await db.QueryAsync<CategoryDTO>("select * from Categories");
        return categories;
    }
}
