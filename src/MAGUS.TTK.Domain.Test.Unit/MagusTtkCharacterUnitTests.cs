using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Domain.Character;
using RPG.Domain.Character;
using MAGUS.TTK.Domain.Character.DTOs;
using MAGUS.TTK.Domain.Character.DTOs.Serializer;

namespace MAGUS.TTK.Domain.Test.Unit
{
    [TestClass]
    public class MagusTtkCharacterUnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new Character.MagusTtkCharacterDto()
            {
                Abilities = new MagusTtkCharacterAbilitiesDto()
                {
                    Ero = new IntValueWithDiceModifierDto() { Value = 9 },
                    Gyo = new IntValueWithDiceModifierDto() { Value = 14, DiceModifier = 1 },
                    Ugy = new IntValueWithDiceModifierDto() { Value = 17 },
                    All = new IntValueWithDiceModifierDto() { Value = 9 },
                    Ege = new IntValueWithDiceModifierDto() { Value = 10 },
                    Kar = new IntValueWithDiceModifierDto() { Value = 11 },
                    Int = new IntValueWithDiceModifierDto() { Value = 11 },
                    Aka = new IntValueWithDiceModifierDto() { Value = 12, DiceModifier = -1 },
                    Asz = new IntValueWithDiceModifierDto() { Value = 10 },
                    Erz = new IntValueWithDiceModifierDto() { Value = 14 },
                },
                Age = 29,
                Class = "Tolvaj",
                Description = null,
                Gender = GenderEnum.Male,
                Level = 5,
                Name = "Mesterlövész Tolvaj",
                Origin = new Background<AttributeBase>()
                {
                    Name = "erioni"
                },
                Race = "Ember",
                Resistances = new MagusTtkCharacterResistancesDto()
                {
                    Asztralis = new IntValueWithDiceModifierDto() { Value = 10 },
                    Elkerulo = new IntValueWithDiceModifierDto() { Value = 14 },
                    Fizikai = new IntValueWithDiceModifierDto() { Value = 10 },
                    Mentalis = new IntValueWithDiceModifierDto() { Value = 12 },
                    Osszetett = new IntValueWithDiceModifierDto() { Value = 12 },
                    Szellemi = new IntValueWithDiceModifierDto() { Value = 12 },
                },
                //Skills = 
                Stats = new MagusTtkCharacterStatsDto()
                {
                    KE = new StatsValue() { Value = 43 },
                    TE = new StatsValue() { Value = 42 },
                    VE = new StatsValue() { Value = 117 },
                    CE = new StatsValue() { Value = 60 },

                    Ep = new StatsValue() { Value = 11 },
                    Fp = new StatsValue() { Value = 28 },

                    Pp = new StatsValue() { Value = 14 },
                    Mp = new StatsValue() { Value = 25 },
                    Kp = new StatsValue() { Value = 0 },

                    Sp = new StatsValue() { Value = 1 },
                    Lp = new StatsValue() { Value = 4 },
                },
                //Talents = 
                //Upbringing = new Background<RPG.Domain.Definitions.AbilityDefinition>
            };

            var jsonString = Utils.SerializeToJsonString(c);

            //c.Stats.KE.Value
        }
    }
}
