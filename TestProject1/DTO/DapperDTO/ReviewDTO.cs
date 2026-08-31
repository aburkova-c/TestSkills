namespace apitest.DTO.DapperDTO;

public class ReviewDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public string CreatedAt { get; set; }
}
