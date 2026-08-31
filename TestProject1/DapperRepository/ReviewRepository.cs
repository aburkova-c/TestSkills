using apitest.DTO.DapperDTO;
using apitest.Interfaces.DapperInterfaces;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class ReviewRepository : IReviewRepository
{
    private readonly string _connectionString;

    public ReviewRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<ReviewDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(_connectionString);
        var reviews = await db.QueryAsync<ReviewDTO>("select * from Reviews");
        return reviews;
    }
}
