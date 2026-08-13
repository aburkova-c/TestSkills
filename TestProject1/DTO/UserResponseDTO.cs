using System.Text.Json.Serialization;
namespace apitest.DTO;

public class UserResponseDTO
{
    [JsonPropertyName("data")]
    public UserDataDTO Data { get; set; }
}