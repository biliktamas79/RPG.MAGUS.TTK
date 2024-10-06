using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RPG.Domain.DiceRoll;

public class DiceRollFormula
{
    private static void ValidateDiceSymbol(char diceSymbol)
    {
        if (char.IsWhiteSpace(diceSymbol) || char.IsNumber(diceSymbol) || diceSymbol == '-' || diceSymbol == '+')
            throw new ArgumentException($"Dice symbol '{diceSymbol}' is invalid. It can't equal to '+', '-', any number or whitespace characters.");
    }

    private static char _defaultDiceSymbol = 'd';
    public static char DefaultDiceSymbol
    {
        get { return _defaultDiceSymbol; }
        set
        {
            ValidateDiceSymbol(value);
            _defaultDiceSymbol = value;
        }
    }
    
    public static DiceRollFormula Parse(string formula)
    {
        return Parse(formula, DefaultDiceSymbol, NumberFormatInfo.InvariantInfo);
    }

    public static DiceRollFormula Parse(string formula, char diceSymbol)
    {
        return Parse(formula, diceSymbol, NumberFormatInfo.InvariantInfo);
    }

    public static DiceRollFormula Parse(string formula, IFormatProvider formatProvider, NumberStyles numberStyles = NumberStyles.Integer)
    {
        return Parse(formula, DefaultDiceSymbol, NumberFormatInfo.InvariantInfo, numberStyles);
    }

    public static DiceRollFormula Parse(string formula, char diceSymbol, IFormatProvider formatProvider, NumberStyles numberStyles = NumberStyles.Integer)
    {
        ValidateDiceSymbol(diceSymbol);

        if (string.IsNullOrWhiteSpace(formula))
            throw new ArgumentException("String is empty or contains whitespace characters only.", nameof(formula));
        if (formula.Length < 2)
            throw new FormatException("String is too short, minimum length is 2. Valid formats are '{DiceSymbol}{DiceSides}+{ConstAddition}', '{DiceSymbol}{DiceSides}-{ConstAddition}', '{NumberOfDices}{DiceSymbol}{DiceSides}+{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}-{ConstAddition}'.");

        int diceSymbolIndex = formula.IndexOf(diceSymbol);
        // if no dice symbol found
        if (diceSymbolIndex < 0)
            throw new FormatException($"Dice symbol '{diceSymbol}' not found. Valid formats are '{{DiceSymbol}}{{DiceSides}}+{{ConstAddition}}', '{{DiceSymbol}}{{DiceSides}}-{{ConstAddition}}', '{{NumberOfDices}}{{DiceSymbol}}{{DiceSides}}+{{ConstAddition}}' or '{{NumberOfDices}}{{DiceSymbol}}{{DiceSides}}-{{ConstAddition}}'.");

        int plusSymbolIndex = formula.IndexOf('+');
        int minusSymbolIndex = formula.IndexOf('-');
        // if both plus and minus symbols are found
        if (plusSymbolIndex >= 0 && minusSymbolIndex >= 0)
            throw new FormatException("Both plus and minus symbols were found. Dice roll formulas can only contain either one plus or one minus sign but not both. Valid formats are '{DiceSymbol}{DiceSides}+{ConstAddition}', '{DiceSymbol}{DiceSides}-{ConstAddition}', '{NumberOfDices}{DiceSymbol}{DiceSides}+{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}-{ConstAddition}'.");

        int constAdditionIndex = -1;
        if (plusSymbolIndex >= 0)
        {
            if (plusSymbolIndex < diceSymbolIndex)
                throw new FormatException("Plus symbol cannot precede the dice symbol. Valid formats are '{DiceSymbol}{DiceSides}+{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}+{ConstAddition}'.");
            // if the formula ends with the plus symbol
            if (plusSymbolIndex == formula.Length - 1)
                throw new FormatException("Formula cannot end with '+' symbol. Valid formats are '{DiceSymbol}{DiceSides}+{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}+{ConstAddition}'.");
            if (plusSymbolIndex == diceSymbolIndex + 1)
                throw new FormatException("DiceSides not found between DiceSymbol and '+' symbol. Valid formats are '{DiceSymbol}{DiceSides}-{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}-{ConstAddition}'.");

            constAdditionIndex = plusSymbolIndex;
        }
        else if (minusSymbolIndex >= 0)
        {
            if (minusSymbolIndex < diceSymbolIndex)
                throw new FormatException("Minus symbol cannot precede the dice symbol. Valid formats are '{DiceSymbol}{DiceSides}-{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}-{ConstAddition}'.");
            // if the formula ends with the minus symbol
            if (minusSymbolIndex == formula.Length - 1)
                throw new FormatException("Formula cannot end with '-' symbol. Valid formats are '{DiceSymbol}{DiceSides}-{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}-{ConstAddition}'.");
            if (minusSymbolIndex == diceSymbolIndex + 1)
                throw new FormatException("DiceSides not found between DiceSymbol and '-' symbol. Valid formats are '{DiceSymbol}{DiceSides}-{ConstAddition}' or '{NumberOfDices}{DiceSymbol}{DiceSides}-{ConstAddition}'.");

            constAdditionIndex = minusSymbolIndex;
        }

        int numberOfDices = (diceSymbolIndex == 0)
            ? 1
            : int.Parse(formula.Substring(0, diceSymbolIndex), numberStyles, formatProvider);

        int diceSidesStartIndex = diceSymbolIndex + 1;
        int diceSidesEndIndex = constAdditionIndex == -1 ? formula.Length : constAdditionIndex;
        int diceSides = int.Parse(formula.Substring(diceSidesStartIndex, diceSidesEndIndex - diceSidesStartIndex), numberStyles, formatProvider);

        int constAddition = (constAdditionIndex <= 0)
            ? 0
            : int.Parse(formula.Substring(constAdditionIndex, formula.Length - constAdditionIndex), numberStyles, formatProvider);

        return new DiceRollFormula(numberOfDices, diceSides, constAddition);
    }

    public DiceRollFormula(int numberOfDices, int diceSides, int constAddition)
    {
        if (numberOfDices <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfDices), "Must be greater than zero.");
        if (numberOfDices >= 1001)
            throw new ArgumentOutOfRangeException(nameof(numberOfDices), "Must be smaller than 1001.");
        if (diceSides <= 1)
            throw new ArgumentOutOfRangeException(nameof(diceSides), "Must be greater than one.");
        if (diceSides >= 1001)
            throw new ArgumentOutOfRangeException(nameof(diceSides), "Must be smaller than 1001.");

        NumberOfDices = numberOfDices;
        DiceSides = diceSides;
        ConstAddition = constAddition;

        _minValue = numberOfDices + constAddition;
        _maxValue = (numberOfDices * diceSides) + constAddition;
    }

    private int _minValue;
    private int _maxValue;

    [Range(1, 1001)]
    public int NumberOfDices { get; set; } = 1;
    [Range(1, 1001)]
    public int DiceSides { get; set; } = 6;
    public int ConstAddition { get; set; }

    public override string ToString()
    {
        return ConstAddition == 0
            ? $"{NumberOfDices}{DefaultDiceSymbol}{DiceSides} (Range: {_minValue}-{_maxValue}, Avg: {CalculateExpectedValue():R})"
            : $"{NumberOfDices}{DefaultDiceSymbol}{DiceSides}{ConstAddition:+#;-#;0} (Range: {_minValue}-{_maxValue}, Avg: {CalculateExpectedValue():R})";
    }

    public int Roll(Random random, IDiceRollResultFactory diceRollResultFactory, IDiceRollSummarizer summarizer)
    {
        var result = diceRollResultFactory.Roll(this);
        return summarizer.Sum(result);
    }

    private decimal CalculateExpectedValue()
    {
        return (_maxValue - _minValue) / 2m + _minValue + ConstAddition;
    }
}
