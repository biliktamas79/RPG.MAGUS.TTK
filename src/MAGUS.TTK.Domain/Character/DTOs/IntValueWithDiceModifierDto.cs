using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// Egy egész szám érték és a hozzá tartozó dobás módosító
    /// </summary>
    public class IntValueWithDiceModifierDto
    {
        /// <summary>
        /// Az érték
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// A dobás módosító
        /// </summary>
        public int? DiceModifier { get; set; }

        public override string ToString()
        {
            return this.DiceModifier == null
                ? $"{this.Value}"
                : $"{this.Value} (+{this.DiceModifier})";
        }
    }
}
