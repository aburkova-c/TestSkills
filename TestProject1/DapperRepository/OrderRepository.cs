using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<OrderDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var orders = await db.QueryAsync<OrderDTO>("select * from Orders");
        return orders;
    }

    public async Task<OrderDTO?> GetByIdAsync(int id)
    {
        using var db = new SqliteConnection(_connectionString);
        var order = await db.QueryFirstOrDefaultAsync<OrderDTO>(
            "select * from Orders where Id = @Id", new { Id = id });
        return order;
    }
}
