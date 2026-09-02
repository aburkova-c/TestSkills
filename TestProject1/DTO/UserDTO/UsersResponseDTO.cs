using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTO;

public record UsersResponseDTO(
    [property: JsonPropertyName("data")] List<UserDTO> Data
    );