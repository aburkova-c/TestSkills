namespace apitest.DTO.BookStoreDTO;

public class AddBookRequestDTO
{
    public string UserId { get; set; }
    public List<BookDTO> CollectionOfIsbns { get; set; }
}
