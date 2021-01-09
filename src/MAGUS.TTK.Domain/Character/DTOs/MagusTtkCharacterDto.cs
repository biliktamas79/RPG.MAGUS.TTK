using RPG.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Character
{
    public class MagusTtkCharacterDto
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
        /// A karakter tulajdonságai
        /// </summary>
        public MagusTtkCharacterAbilitiesDto Abilities { get; set; }

        /// <summary>
        /// A karakter statisztikái
        /// </summary>
        public MagusTtkCharacterStatsDto Stats { get; set; }

        /// <summary>
        /// A karakter ellenállásai
        /// </summary>
        public MagusTtkCharacterResistancesDto Resistances { get; set; }

        /// <summary>
        /// Adottságok
        /// </summary>
        public readonly List<TraitValue> Talents = new List<TraitValue>(6);
        /// <summary>
        /// Képzettségek
        /// </summary>
        public readonly Dictionary<string, SkillLevel> Skills = new Dictionary<string, SkillLevel>();
    }
}
