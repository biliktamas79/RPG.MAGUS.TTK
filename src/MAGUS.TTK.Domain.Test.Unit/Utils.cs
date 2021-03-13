using MAGUS.TTK.Domain.Character.DTOs.Serializer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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
            IncludeFields = true,
            Encoder = //System.Text.Encodings.Web.JavaScriptEncoder.Default
                System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All),
        };

        static Utils()
        {
            JsonSerializerOptions.Converters.Add(new IntValueWithDiceModifierDtoSerializer());
            JsonSerializerOptions.Converters.Add(new StatsValueSerializer());
            JsonSerializerOptions.Converters.Add(new CodeOnlyAttributeSerializer());
            //JsonSerializerOptions.Converters.Add(new MagusTtkCharacterSkillLevelsEnumSerializer());
            JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static string SerializeToJsonString<T>(T value)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize<T>(value, JsonSerializerOptions);

            return jsonString;
        }

        public static T DeserializeFromJsonString<T>(string jsonString)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString, JsonSerializerOptions);
        }
    }
}
