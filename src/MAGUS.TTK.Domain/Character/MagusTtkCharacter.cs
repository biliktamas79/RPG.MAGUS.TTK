using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Character;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    public class MagusTtkCharacter
    {
        /// <summary>
        /// A karakter neve
        /// </summary>
        public string Name;
        /// <summary>
        /// A karakter leírása
        /// </summary>
        public string Description;
        /// <summary>
        /// A karakter tapasztalati szintje
        /// </summary>
        public int Level;
        /// <summary>
        /// A karakter faja
        /// </summary>
        public string Race;
        /// <summary>
        /// A karakter kasztja
        /// </summary>
        public string Class;
        /// <summary>
        /// A karakter neme
        /// </summary>
        public GenderEnum Gender;
        /// <summary>
        /// A karakter kora
        /// </summary>
        public int Age;
        /// <summary>
        /// Háttér/Származás
        /// </summary>
        public Background<AttributeBase> Origin;
        /// <summary>
        /// Háttér/Neveltetés
        /// </summary>
        public Background<AbilityDefinition> Upbringing;
        /// <summary>
        /// Jellemvonások
        /// </summary>
        public readonly List<TraitValue> Traits = new List<TraitValue>(6);
        /// <summary>
        /// Tulajdonságok
        /// </summary>
        public readonly Dictionary<MagusTtkCharacterAbilityEnum, AbilityValue> Abilities = new Dictionary<MagusTtkCharacterAbilityEnum, AbilityValue>(10);
        /// <summary>
        /// Statisztika
        /// </summary>
        public readonly Dictionary<MagusTtkCharacterStatsEnum, StatsValue> Stats = new Dictionary<MagusTtkCharacterStatsEnum, StatsValue>(10);
        /// <summary>
        /// Adottságok
        /// </summary>
        public readonly List<TraitValue> Talents = new List<TraitValue>(6);
        /// <summary>
        /// Képzettségek
        /// </summary>
        public readonly Dictionary<string, SkillLevel> Skills = new Dictionary<string, SkillLevel>();

        /// <summary>
        /// Közelharci értékek
        /// </summary>
        public readonly List<MeleeCombatValues> CloseCombatValues = new List<MeleeCombatValues>();
        /// <summary>
        /// Hajító / lőfegyveres harcértékek
        /// </summary>
        public readonly List<RangedCombatValues> RangedCombatValues = new List<RangedCombatValues>();
    }
}
