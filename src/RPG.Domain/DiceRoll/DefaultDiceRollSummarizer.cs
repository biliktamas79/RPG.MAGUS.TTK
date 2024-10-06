using System.Linq;

namespace RPG.Domain.DiceRoll;

public class DefaultDiceRollSummarizer : IDiceRollSummarizer
{
    public int Sum(DiceRollResult diceRollResult)
    {
        return diceRollResult.RolledValues.Sum() + diceRollResult.Formula.ConstAddition;
    }
}
