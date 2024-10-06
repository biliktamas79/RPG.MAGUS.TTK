using System;

namespace RPG.Domain.DiceRoll;

public class RandomDiceRollValueProvider : IDiceRollValueProvider
{
    private readonly Random _random;

    public RandomDiceRollValueProvider()
    {
        _random = new Random();
    }

    public RandomDiceRollValueProvider(int seed)
    {
        _random = new Random(seed);
    }

    public int Roll(int diceSides)
    {
        return _random.Next(1, diceSides + 1);
    }
}
