using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Domain.Definitions;
using RPG.Domain.Definitions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MAGUS.TTK.Domain.Character;
using RPG.Domain.Character;

namespace MAGUS.TTK.Domain.Test.Unit
{
    [TestClass]
    public class MagusTtkDefinitionsUnitTests
    {
        [TestMethod]
        public void JsonSerializeMagusTtkDefinitions()
        {
            var d = new MagusTtkDefinitions()
            {
                Version = "0.8.4"
            };

            var jsonString = Utils.SerializeToJsonString(d);

            //c.Stats.KE.Value
        }

        [TestMethod]
        public void JsonDeserializeMagusTtkDefinitions()
        {
            //FileInfo srcJsonFile = new FileInfo(Path.GetFullPath("abilities.json"));
            string jsonString = File.ReadAllText(Path.Combine("Definitions", "abilities.json"));

            var deserializedAbilities = Utils.DeserializeFromJsonString<MagusTtkDefinitions>(jsonString);

            Assert.IsNotNull(deserializedAbilities);
            Assert.IsNotNull(deserializedAbilities.Version);
            Assert.IsFalse(string.IsNullOrWhiteSpace(deserializedAbilities.Version));
        }

        [TestMethod]
        public void JsonDeserializeMagusTtkDefinitionsOrigins()
        {
            //FileInfo srcJsonFile = new FileInfo(Path.GetFullPath("abilities.json"));
            string jsonString = File.ReadAllText(Path.Combine("Definitions", "origins.json"));

            var deserialized = Utils.DeserializeFromJsonString<Dictionary<string, Background<CodeOnlyAttribute>>>(jsonString);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.Count > 0);
            Assert.IsTrue(deserialized.All(kvp => !string.IsNullOrWhiteSpace(kvp.Key) && AssertDeserializedOriginIsNotNullAndValid(kvp.Value)));
        }

        private static bool AssertDeserializedOriginIsNotNullAndValid(Background<CodeOnlyAttribute> origin)
        {
            return (origin != null)
                && !string.IsNullOrWhiteSpace(origin.Code)
                && !string.IsNullOrWhiteSpace(origin.Name)
                && (origin.Advantages?.Any() ?? false)
                && (origin.Disadvantages?.Any() ?? false);
        }
    }
}
