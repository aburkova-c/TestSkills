using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterfaces;

public interface IReviewRepository
{
    Task<IEnumerable<ReviewDTO>> GetAllAsync();
}
