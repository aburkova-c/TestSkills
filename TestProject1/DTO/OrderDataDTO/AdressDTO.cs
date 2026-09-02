using System.Text.Json.Serialization;

namespace apitest.DTO.OrderDataDTO;

public record AdressDTO(
    [property: JsonPropertyName("country")] 
    string Country,
    [property: JsonPropertyName("city")]
    string City,
    [property: JsonPropertyName("street")]
    string Street,
    [property: JsonPropertyName("zip")]
    string Zip
    );