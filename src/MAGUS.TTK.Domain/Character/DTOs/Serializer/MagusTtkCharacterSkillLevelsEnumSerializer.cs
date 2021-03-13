using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAGUS.TTK.Domain.Character.DTOs.Serializer
{
    public class MagusTtkCharacterSkillLevelsEnumSerializer : JsonConverter<MagusTtkCharacterSkillLevelsEnum>
    {
        public override MagusTtkCharacterSkillLevelsEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // ha csak egy Int32 van a JSON-ban
            if (reader.TryGetInt32(out int value))
            {
                return (MagusTtkCharacterSkillLevelsEnum)value;
            }
            
            // különben string-ként az Enum value-ra számítunk
            var stringValue = reader.GetString();
            // ha nem sikerül enum-ként értelmezni
            if (!Enum.TryParse<MagusTtkCharacterSkillLevelsEnum>(stringValue, out var enumValue))
                throw new ArgumentException($"Could not parse '{stringValue}' as a {nameof(MagusTtkCharacterSkillLevelsEnum)} value.");

            return enumValue;
        }

        public override void Write(Utf8JsonWriter writer, MagusTtkCharacterSkillLevelsEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
