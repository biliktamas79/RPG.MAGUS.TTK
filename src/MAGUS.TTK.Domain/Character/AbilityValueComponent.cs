using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képesség, mint Erő, Ügy, ...
    /// </summary>
    public class AbilityValueComponent
    {
        public readonly AbilityDefinition Definition;
        public readonly AbilityValueComponentTypeEnum Type;
        public int Value;
    }
}
