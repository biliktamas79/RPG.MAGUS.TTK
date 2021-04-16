using System;
using System.Collections.Generic;
using System.Text;
using MAGUS.TTK.Domain.Definitions;
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
        public MagusTtkRaceDefinition Race;
        /// <summary>
        /// A karakter kasztja
        /// </summary>
        public MagusTtkCharacterClassDefinition Class;
        /// <summary>
        /// A karakter neme
        /// </summary>
        public GenderEnum Gender;
        /// <summary>
        /// A karakter kora
        /// </summary>
        public decimal Age;
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
        public readonly Dictionary<string, AbilityValue> Abilities = new Dictionary<string, AbilityValue>(10);
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
        public readonly MagusTtkCharacterSkills Skills = new MagusTtkCharacterSkills();

        /// <summary>
        /// Közelharci értékek
        /// </summary>
        public readonly List<MeleeCombatValues> CloseCombatValues = new List<MeleeCombatValues>();
        /// <summary>
        /// Hajító / lőfegyveres harcértékek
        /// </summary>
        public readonly List<RangedCombatValues> RangedCombatValues = new List<RangedCombatValues>();

        /// <summary>
        /// Kiszámolja, hogy ez a karakter a korából fakadóan eddig összesen hányszor kellett már öregedési próbát dobnia.
        /// </summary>
        /// <returns></returns>
        public int GetSumAgingRollsCount()
        {
            return this.Race?.Aging?.GetAgingRollsCount(this.Age) ?? 0;
        }

        /// <summary>
        /// Kiszámolja, hogy ez a karakter a korából fakadóan összesen hány Sp-t kap.
        /// </summary>
        /// <returns></returns>
        public int GetSumSp()
        {
            return this.Race?.Aging?.GetFreeSp(this.Age) ?? 0;
        }

        public int? GetAbilityValueMin(string abilityCode)
        {
            if (!this.Abilities.TryGetValue(abilityCode, out var av))
                return null;

            int? min = av?.Definition?.MinValue ?? null;
            if (min == null)
                return null;

            // ha tudjuk már milyen kaszt
            if (this.Class != null)
            {
                // akkor a minimumot a kasztnál megadott értékre állítjuk
                if (this.Class.Abilities.TryGetValue(abilityCode, out var classAbilityValue))
                    min = classAbilityValue;
            }

            //// ha be lett már állítva a faj
            //if (this.Race != null)
            //{
            //    // akkor érvényesítjük a faji módosítókat is
            //    if (this.Race.AbilityModifiers.TryGetValue(abilityCode, out var raceModifier))
            //        min += raceModifier;
            //}

            return min;
        }

        public int? GetAbilityValueMax(string abilityCode)
        {
            if (!this.Abilities.TryGetValue(abilityCode, out var av))
                return null;

            int? max = av?.Definition?.MaxValue ?? null;
            if (max == null)
                return null;

            // ha be lett már állítva a faj
            if (this.Race != null)
            {
                // akkor érvényesítjük a faji módosítókat is
                if (this.Race.AbilityModifiers.TryGetValue(abilityCode, out var raceModifier))
                    max += raceModifier;
            }

            return max;
        }
    }
}
