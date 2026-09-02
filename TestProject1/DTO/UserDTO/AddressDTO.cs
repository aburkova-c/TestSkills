using System.Text.Json.Serialization;

namespace apitest.DTO.UserDTO;

public record AddressDTO(
    [property: JsonPropertyName("street")] string Street,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("geo")] GeoDTO Geo
    );