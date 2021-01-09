using MAGUS.TTK.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    public class MagusTtkDefinitions
    {
        public Version Version { get; set; }

        /// <summary>
        /// Karakter Tulajdonság definíciók (Erő, Gyo, Ügy, ...)
        /// </summary>
        public readonly Dictionary<MagusTtkCharacterAbilityEnum, AbilityDefinition> CharacterAbilities = new Dictionary<MagusTtkCharacterAbilityEnum, AbilityDefinition>(10)
        {
            { MagusTtkCharacterAbilityEnum.Strength, new AbilityDefinition() { Code = "Erő", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Quickness, new AbilityDefinition() { Code = "Gyo", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Dexterity, new AbilityDefinition() { Code = "Ügy", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Stamina, new AbilityDefinition() { Code = "Áll", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Health, new AbilityDefinition() { Code = "Egé", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Charisma, new AbilityDefinition() { Code = "Kar", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Intelligence, new AbilityDefinition() { Code = "Int", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Will, new AbilityDefinition() { Code = "Aka", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Astral, new AbilityDefinition() { Code = "Asz", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Perception, new AbilityDefinition() { Code = "Érz", MinValue = 6, MaxValue = 20 } },
        };
    }
}
