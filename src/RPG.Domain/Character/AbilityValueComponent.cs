using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace RPG.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képesség, mint Erő, Ügy, ...
    /// </summary>
    public class AbilityValueComponent<TabilityValueComponentType>
    {
        public readonly AbilityDefinition Definition;
        public readonly TabilityValueComponentType Type;
        public int Value;
    }
}
