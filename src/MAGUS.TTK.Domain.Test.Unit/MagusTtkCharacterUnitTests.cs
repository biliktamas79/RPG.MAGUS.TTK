using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Domain.Character;
using RPG.Domain.Character;

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
                    KE = new StatsValue(),
                    TE = new StatsValue(),
                    VE = new StatsValue(),
                    CE = new StatsValue(),

                    Ep = new StatsValue(),
                    Fp = new StatsValue(),

                    Pp = new StatsValue(),
                    Mp = new StatsValue(),
                    Kp = new StatsValue(),

                    Sp = new StatsValue(),
                    Lp = new StatsValue(),
                },
                //Talents = 
                //Upbringing = new Background<RPG.Domain.Definitions.AbilityDefinition>
            };

            //c.Stats.KE.Value
        }
    }
}
