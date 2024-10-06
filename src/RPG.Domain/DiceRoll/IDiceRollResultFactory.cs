namespace RPG.Domain.DiceRoll;

public interface IDiceRollResultFactory
{
    DiceRollResult Roll(DiceRollFormula formula);
}
