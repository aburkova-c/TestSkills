using System.Text.Json.Serialization;

namespace apitest.DTO.OrderDataDTO;

public record ItemDTO(
    [property: JsonPropertyName("productId")] int ProductID,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("quantity")] int Quantity,
    [property: JsonPropertyName("price")] decimal Price
    );