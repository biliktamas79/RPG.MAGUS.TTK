using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of a weapon
    /// </summary>
    public class WeaponDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Weapon unique code, like ShortBlade, ThrowingKnife, ...
        /// </summary>
        public string Code { get; private set; }
        public WeaponCategory Category { get; set; }

        public int Range { get; set; }
    }
}
