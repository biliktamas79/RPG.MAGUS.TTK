using RPG.Domain.Character;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAGUS.TTK.Domain.Character.DTOs.Serializer
{
    public class CodeOnlyAttributeSerializer : JsonConverter<CodeOnlyAttribute>
    {
        public override CodeOnlyAttribute Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();

            // ha csak egy string van a JSON-ban
            if (!string.IsNullOrEmpty(s))
            {
                return new CodeOnlyAttribute(s);
            }

            // különben az egész DTO-ra számítunk
            return JsonSerializer.Deserialize<CodeOnlyAttribute>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, CodeOnlyAttribute value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                //writer.WriteStartObject();
                //writer.WriteString(nameof(CodeOnlyAttribute.Code), value.Code);
                //writer.WriteEndObject();

                writer.WriteStringValue(value.Code);
            }
        }
    }
}
