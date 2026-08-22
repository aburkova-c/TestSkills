using System.Text.Json.Serialization;

namespace apitest.DTO.OrderDataDTO;

public record PaymentDTO(
    [property: JsonPropertyName("method")] string Method,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("transactionId")] string transactionId
    );