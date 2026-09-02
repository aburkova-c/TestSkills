using System.Text.Json.Serialization;

namespace apitest.DTO.OrderDataDTO;

public record DeliveryDTO(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("estimatedDate")] string EstimatedDate,
    [property: JsonPropertyName("trackingNumber")] string TrackingNumber
    );