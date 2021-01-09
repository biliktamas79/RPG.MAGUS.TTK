using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Category of a weapon
    /// </summary>
    public class WeaponCategory : IHasUniqueCode
    {
        /// <summary>
        /// Weapon category unique code, like Melee.Blades.Short, or LongBlades, or ThrowingBlades, ...
        /// </summary>
        public string Code { get; private set; }
        public string Name;
    }
}
