using System.Text.Json.Serialization;

namespace apitest.DTO.BookStoreDTO;

public class UserDTO
{
    [JsonPropertyName("userName")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}