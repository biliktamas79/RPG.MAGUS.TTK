namespace RPG.Domain.DiceRoll;

public interface IDiceRollValueProvider
{
    int Roll(int diceSides);
}
