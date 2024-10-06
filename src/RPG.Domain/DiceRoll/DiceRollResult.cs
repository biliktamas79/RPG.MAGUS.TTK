namespace RPG.Domain.DiceRoll;

public class DiceRollResult
{
    public readonly DiceRollFormula Formula;

    public DiceRollResult(DiceRollFormula formula, int[] rolledValues)
    {
        Formula = formula;
        RolledValues = rolledValues;
    }

    public int[] RolledValues { get; set; }
    public int RolledDicesCount => RolledValues.Length;
}
