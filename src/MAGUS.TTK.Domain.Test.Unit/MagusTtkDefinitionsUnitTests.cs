using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Domain.Definitions;
using RPG.Domain.Definitions;
using System.IO;

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

            var deserializedAbilities = System.Text.Json.JsonSerializer.Deserialize<MagusTtkDefinitions>(jsonString, Utils.JsonSerializerOptions);

            Assert.IsNotNull(deserializedAbilities);
            Assert.IsNotNull(deserializedAbilities.Version);
            Assert.IsFalse(string.IsNullOrWhiteSpace(deserializedAbilities.Version));
        }
    }
}
