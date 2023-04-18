using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScalesApi.Utils;

/// <summary>
/// Преобразует <see cref="DateTime">DateTime</see> в формат "день.месяц.год час:минут:секунд".
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("G"));
    }
}