using Microsoft.Extensions.DependencyInjection;
using RPG.Domain.DiceRoll;

namespace RPG.Domain;

/// <summary>
/// Static class containing extensions for interfaces
/// </summary>
public static partial class Extensions
{
    /// <summary>
    /// Registers default implementations for <see cref="IDiceRollValueProvider"/>, <see cref="IDiceRollResultFactory"/> and <see cref="IDiceRollSummarizer"/> dice roll service interfaces.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterDefaultDiceRollServices(this IServiceCollection services)
    {
        services.AddSingleton<IDiceRollValueProvider, RandomDiceRollValueProvider>();
        services.AddSingleton<IDiceRollResultFactory, DefaultDiceRollResultFactory>();
        services.AddSingleton<IDiceRollSummarizer, DefaultDiceRollSummarizer>();

        return services;
    }
}
