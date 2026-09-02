using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTO;

public record GeoDTO(
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonPropertyName("lng")] double Lng
    );