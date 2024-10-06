namespace RPG.Domain.DiceRoll;

public class DefaultDiceRollResultFactory : IDiceRollResultFactory
{
    private readonly IDiceRollValueProvider _diceRollValueProvider;

    public DefaultDiceRollResultFactory(IDiceRollValueProvider diceRollValueProvider)
    {
        this._diceRollValueProvider = diceRollValueProvider;
    }

    public DiceRollResult Roll(DiceRollFormula formula)
    {
        int[] rolledValues = new int[formula.NumberOfDices];

        for (int i = 0; i < rolledValues.Length; i++)
        {
            rolledValues[i] = _diceRollValueProvider.Roll(formula.DiceSides);
        }

        return new DiceRollResult(formula, rolledValues);
    }
}
