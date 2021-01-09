using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képesség, mint Erő, Ügy, ...
    /// </summary>
    public class AbilityValue
    {
        /// <summary>
        /// Tulajdonság definíció
        /// </summary>
        public AbilityDefinition Definition { get; private set; }

        /// <summary>
        /// Tulajdonság módosítók típus szerint
        /// </summary>
        public readonly Dictionary<AbilityValueComponentTypeEnum, AbilityValueComponent> ValueComponentsByType = new Dictionary<AbilityValueComponentTypeEnum, AbilityValueComponent>(5);

        /// <summary>
        /// A tulajdonság értéke
        /// </summary>
        public int Value { get; }
    }
}
