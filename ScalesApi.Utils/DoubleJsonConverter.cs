using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScalesApi.Utils;

/// <summary>
/// Преобразует <see cref="double">double</see> в строку с запятой в качестве разделителя между целой и дробной частью.
/// </summary>
public class DoubleJsonConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().Replace('.', ','));
    }
}