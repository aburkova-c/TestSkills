using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTO;

public record ProfileDTO(
    [property: JsonPropertyName("fullName")] string FullName,
    [property: JsonPropertyName("age")] int Age,
    [property: JsonPropertyName("address")] AddressDTO Address,
    [property: JsonPropertyName("tags")] List<string> Tags
    );