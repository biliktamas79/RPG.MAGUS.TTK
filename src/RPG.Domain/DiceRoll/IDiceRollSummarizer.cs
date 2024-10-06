namespace RPG.Domain.DiceRoll;

public interface IDiceRollSummarizer
{
    /// <summary>
    /// Calculates the value of the given dice roll result.
    /// </summary>
    /// <param name="diceRollResult"></param>
    /// <returns></returns>
    int Sum(DiceRollResult diceRollResult);
}
