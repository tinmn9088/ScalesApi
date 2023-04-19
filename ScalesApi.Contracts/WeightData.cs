using System.Text.Json.Serialization;
using ScalesApi.Utils;

namespace ScalesApi.Contracts;

public record WeightData
{
    public double Weight { get; init; }
    public string Unit { get; init; }
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime Timestamp { get; init; }

    public WeightData(double weight, string unit) 
    { 
        Weight = weight; 
        Unit = unit; 
        Timestamp = DateTime.Now;
    }
}