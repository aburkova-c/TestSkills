using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly string _connectionString;

    public OrderItemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var orderItems = await db.QueryAsync<OrderItemDTO>("select * from OrderItems");
        return orderItems;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(int orderId)
    {
        using var db = new SqliteConnection(_connectionString);
        var orderItems = await db.QueryAsync<OrderItemDTO>(
            "select * from OrderItems where OrderId = @OrderId", new { OrderId = orderId });
        return orderItems;
    }
}
