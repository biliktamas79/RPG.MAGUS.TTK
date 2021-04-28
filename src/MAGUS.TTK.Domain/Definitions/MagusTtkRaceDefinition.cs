using MAGUS.TTK.Domain.Character;
using RPG.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Definition of a race in MAGUS TTK.
    /// </summary>
    public class MagusTtkRaceDefinition : RaceDefinition
    {
        public string Description { get; set; }

        public AgingCategory[] Aging { get; set; }

        /// <summary>
        /// Tulajdonság módosítók
        /// </summary>
        public Dictionary<string, int> AbilityModifiers;

        ///// <summary>
        ///// Abilities this skill is based on. Ability requirements for reaching certain levels of this skill are based on these.
        ///// </summary>
        //public MagusTtkTalentLevelDefinition[] Levels { get; set; }

        public int? GetAdventureStartingAge()
        {
            return this.Aging?.FirstOrDefault(r => (r.SpPerTimeUnit > 0) || (r.SpPerYear > 0))?.FromAge;
        }
    }
}
