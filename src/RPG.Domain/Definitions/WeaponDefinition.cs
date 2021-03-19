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
        public string Code { get; set; }
        /// <summary>
        /// Weapon category.
        /// </summary>
        public WeaponCategory Category { get; set; }
        /// <summary>
        /// Weapon description.
        /// </summary>
        public string Description { get; set; }
    }
}
