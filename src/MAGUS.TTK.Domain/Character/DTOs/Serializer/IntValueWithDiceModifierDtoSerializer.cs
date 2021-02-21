using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAGUS.TTK.Domain.Character.DTOs.Serializer
{
    public class IntValueWithDiceModifierDtoSerializer : JsonConverter<IntValueWithDiceModifierDto>
    {
        //public override bool CanConvert(Type typeToConvert)
        //{
        //    return (typeToConvert == typeof(IntValueWithDiceModifierDto) ||
        //            typeToConvert == typeof(int));
        //}

        public override IntValueWithDiceModifierDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // ha csak egy Int32 van a JSON-ban
            if (reader.TryGetInt32(out int value))
            {
                return new IntValueWithDiceModifierDto() { Value = value, DiceModifier = 0 };
            }

            // különben az egész DTO-ra számítunk
            return JsonSerializer.Deserialize<IntValueWithDiceModifierDto>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IntValueWithDiceModifierDto value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else if (!value.DiceModifier.HasValue || (value.DiceModifier == 0))
            {
                writer.WriteNumberValue(value.Value);
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteNumber(nameof(IntValueWithDiceModifierDto.Value), value.Value);
                if (value.DiceModifier.HasValue)
                    writer.WriteNumber(nameof(IntValueWithDiceModifierDto.DiceModifier), value.DiceModifier.Value);
                else
                    writer.WriteNull(nameof(IntValueWithDiceModifierDto.DiceModifier));
                writer.WriteEndObject();
            }
        }
    }
}
