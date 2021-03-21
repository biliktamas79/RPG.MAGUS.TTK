using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter adottság növekmény
    /// </summary>
    public class TalentIncrease
    {
        /// <summary>
        /// Adottság definíció
        /// </summary>
        public TalentDefinition Definition { get; set; }

        /// <summary>
        /// Az adottság érték növekménye
        /// </summary>
        public int Value => 1;

        public override string ToString()
        {
            return $"{this.Definition.Code} +{this.Value}";
        }
    }
}
