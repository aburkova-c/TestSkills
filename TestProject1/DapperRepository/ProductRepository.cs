using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var products = await db.QueryAsync<ProductDTO>("select * from Products");
        return products;
    }

    public async Task<ProductDTO?> GetByIdAsync(int id)
    {
        using var db = new SqliteConnection(_connectionString);
        var product = await db.QueryFirstOrDefaultAsync<ProductDTO>(
            "select * from Products where Id = @Id", new { Id = id });
        return product;
    }
}
