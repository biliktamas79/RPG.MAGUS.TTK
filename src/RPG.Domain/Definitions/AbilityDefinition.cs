using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of a character ability
    /// </summary>
    public class AbilityDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Ability unique code, like STR, WILL, ...
        /// </summary>
        public string Code { get; /*private*/ set; }
        public int MinValue;
        public int MaxValue;
    }
}
