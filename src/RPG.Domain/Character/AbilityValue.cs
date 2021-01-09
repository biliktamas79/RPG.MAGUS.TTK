using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace RPG.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képesség, mint Erő, Ügy, ...
    /// </summary>
    public class AbilityValue<TabilityValueComponentType>
    {
        /// <summary>
        /// Tulajdonság definíció
        /// </summary>
        public AbilityDefinition Definition { get; set; }

        /// <summary>
        /// Tulajdonság módosítók típus szerint
        /// </summary>
        public readonly Dictionary<TabilityValueComponentType, AbilityValueComponent<TabilityValueComponentType>> ValueComponentsByType = new Dictionary<TabilityValueComponentType, AbilityValueComponent<TabilityValueComponentType>>(5);

        /// <summary>
        /// A tulajdonság értéke
        /// </summary>
        public int Value { get; }
    }
}
