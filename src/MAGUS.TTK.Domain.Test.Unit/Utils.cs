using MAGUS.TTK.Domain.Character.DTOs.Serializer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Test.Unit
{
    internal static class Utils
    {
        internal readonly static System.Text.Json.JsonSerializerOptions JsonSerializerOptions = new System.Text.Json.JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip,
            WriteIndented = true,
            IncludeFields = true
        };

        static Utils()
        {
            JsonSerializerOptions.Converters.Add(new IntValueWithDiceModifierDtoSerializer());
            JsonSerializerOptions.Converters.Add(new StatsValueSerializer());
        }

        public static string SerializeToJsonString<T>(T value)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize<T>(value, JsonSerializerOptions);

            return jsonString;
        }
    }
}
