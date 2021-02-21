using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAGUS.TTK.Domain.Character.DTOs.Serializer
{
    public class StatsValueSerializer : JsonConverter<StatsValue>
    {
        public override StatsValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // ha csak egy Int32 van a JSON-ban
            if (reader.TryGetInt32(out int value))
            {
                return new StatsValue() { Value = value };
            }

            // különben az egész DTO-ra számítunk
            return JsonSerializer.Deserialize<StatsValue>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, StatsValue value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else if (value.ValueComponentsByType == null || (value.ValueComponentsByType.Count == 0))
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteNumber(nameof(IntValueWithDiceModifierDto.Value), value.Value);
                JsonSerializer.Serialize(value.ValueComponentsByType);
                //if (value.ValueComponentsByType.Count > 0)
                //{
                //    foreach (var kvp in value.ValueComponentsByType)
                //    {
                //        JsonSerializer.Serialize(value.ValueComponentsByType);
                //    }
                //}
                writer.WriteEndObject();
            }
        }
    }
}
