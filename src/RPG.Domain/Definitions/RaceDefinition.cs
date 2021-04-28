using RPG.Domain;
using RPG.Domain.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of a race, like Human, Elf, Dwarf, ...
    /// </summary>
    public class RaceDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Race unique code, like Human, Elf, Dwarf, ...
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The valid genders of this race.
        /// </summary>
        public GenderEnum[] Genders { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}
