using RPG.Domain.DiceRoll;

namespace RPG.Domain.Tests.Unit
{
    public class DiceRollFormulaUnitTests
    {
        public DiceRollFormulaUnitTests()
        {
            DiceRollFormula.DefaultDiceSymbol = 'd';
        }

        [Fact]
        public void DiceRollFormula_Parse_ThrownOnInvalidFormats()
        {
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse(""));    // empty
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse(" \t "));// whitespace-only
            
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("d"));   // too short
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("d6+")); // can't end with plus sign
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("d6-")); // can't end with minus sign
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("a6"));  // dice symbol not found
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("1d6+2-1")); // both plus and minus sign found
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("+1d6")); // plus sign before dice symbol
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("1d+1")); // plus sign right after dice symbol
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("-1d10")); // minus sign before dice symbol
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("1d-1")); // minus sign right after dice symbol
            
            Assert.Throws<ArgumentOutOfRangeException>(() => DiceRollFormula.Parse("1001d10+1")); // number of dices greater than 1000
            Assert.Throws<ArgumentOutOfRangeException>(() => DiceRollFormula.Parse("10d1001+1")); // dice sides greater than 1000
        }

        [Fact]
        public void DiceRollFormula_ParseWithDiceSymbol_ThrownOnInvalidFormats()
        {
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse("", '#'));    // empty
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse(" \t ", '#'));// whitespace-only
            
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("d", '#'));   // too short
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("d6+", '#')); // can't end with plus sign
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("d6-", '#')); // can't end with minus sign
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("a6", '#'));  // dice symbol not found
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("1d6+2-1", '#')); // both plus and minus sign found
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("+1d6", '#')); // plus sign before dice symbol
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("1d+1", '#')); // plus sign right after dice symbol
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("-1d10", '#')); // minus sign before dice symbol
            Assert.Throws<FormatException>(() => DiceRollFormula.Parse("1d-1", '#')); // minus sign right after dice symbol

            Assert.Throws<ArgumentOutOfRangeException>(() => DiceRollFormula.Parse("1001d10+1")); // number of dices greater than 1000
            Assert.Throws<ArgumentOutOfRangeException>(() => DiceRollFormula.Parse("10d1001+1")); // dice sides greater than 1000

            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse("3d10+2", '-')); // dice symbol equals minus sign
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse("3d10+2", '+')); // dice symbol equals plus sign
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse("3d10+2", '4')); // dice symbol is a number
            Assert.Throws<ArgumentException>(() => DiceRollFormula.Parse("3d10+2", ' ')); // dice symbol is whitespace
        }

        [Fact]
        public void DiceRollFormula_Parse_ParsesValidFormats_withDefaultDiceSymbol()
        {
            var diceRollFormula = DiceRollFormula.Parse("d6");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1, diceRollFormula.NumberOfDices);
            Assert.Equal(6, diceRollFormula.DiceSides);
            Assert.Equal(0, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("1d6");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1, diceRollFormula.NumberOfDices);
            Assert.Equal(6, diceRollFormula.DiceSides);
            Assert.Equal(0, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("2d100");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(2, diceRollFormula.NumberOfDices);
            Assert.Equal(100, diceRollFormula.DiceSides);
            Assert.Equal(0, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("1d6-1");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1, diceRollFormula.NumberOfDices);
            Assert.Equal(6, diceRollFormula.DiceSides);
            Assert.Equal(-1, diceRollFormula. ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("10d8+6");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(10, diceRollFormula.NumberOfDices);
            Assert.Equal(8, diceRollFormula.DiceSides);
            Assert.Equal(6, diceRollFormula.ConstAddition);


            DiceRollFormula.DefaultDiceSymbol = 'K';
            diceRollFormula = DiceRollFormula.Parse("6K10+4");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(6, diceRollFormula.NumberOfDices);
            Assert.Equal(10, diceRollFormula.DiceSides);
            Assert.Equal(4, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("1000K1000+1000");
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1000, diceRollFormula.NumberOfDices);
            Assert.Equal(1000, diceRollFormula.DiceSides);
            Assert.Equal(1000, diceRollFormula.ConstAddition);
        }

        [Fact]
        public void DiceRollFormula_ParseWithDiceSymbol_ParsesValidFormats_withDefaultDiceSymbol()
        {
            var diceRollFormula = DiceRollFormula.Parse("#6", '#');
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1, diceRollFormula.NumberOfDices);
            Assert.Equal(6, diceRollFormula.DiceSides);
            Assert.Equal(0, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("1#6", '#');
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1, diceRollFormula.NumberOfDices);
            Assert.Equal(6, diceRollFormula.DiceSides);
            Assert.Equal(0, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("2#100", '#');
            Assert.NotNull(diceRollFormula);
            Assert.Equal(2, diceRollFormula.NumberOfDices);
            Assert.Equal(100, diceRollFormula.DiceSides);
            Assert.Equal(0, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("1@6-1", '@');
            Assert.NotNull(diceRollFormula);
            Assert.Equal(1, diceRollFormula.NumberOfDices);
            Assert.Equal(6, diceRollFormula.DiceSides);
            Assert.Equal(-1, diceRollFormula.ConstAddition);

            diceRollFormula = DiceRollFormula.Parse("10D8+6", 'D');
            Assert.NotNull(diceRollFormula);
            Assert.Equal(10, diceRollFormula.NumberOfDices);
            Assert.Equal(8, diceRollFormula.DiceSides);
            Assert.Equal(6, diceRollFormula.ConstAddition);


            DiceRollFormula.DefaultDiceSymbol = 'K';
            diceRollFormula = DiceRollFormula.Parse("6z10+4", 'z');
            Assert.NotNull(diceRollFormula);
            Assert.Equal(6, diceRollFormula.NumberOfDices);
            Assert.Equal(10, diceRollFormula.DiceSides);
            Assert.Equal(4, diceRollFormula.ConstAddition);
        }
    }
}