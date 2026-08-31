using System.Text.Json.Serialization;

namespace apitest.DTO.BookStoreDTO;

public class LoginRequestDTO
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
