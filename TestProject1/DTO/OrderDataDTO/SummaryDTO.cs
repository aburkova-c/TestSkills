using System.Text.Json.Serialization;

namespace apitest.DTO.OrderDataDTO;

public record SummaryDTO(
    [property: JsonPropertyName("itemsTotal")] decimal ItemsTotal,
    [property: JsonPropertyName("deliveryFee")] decimal DeliveryFee,
    [property: JsonPropertyName("discount")] decimal Discount,
    [property: JsonPropertyName("finalTotal")] decimal FinalTotal
    );