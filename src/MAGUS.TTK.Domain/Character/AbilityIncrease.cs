using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter tulajdonság növekmény
    /// </summary>
    public class AbilityIncrease
    {
        /// <summary>
        /// Tulajdonság definíció
        /// </summary>
        public AbilityDefinition Definition { get; set; }

        /// <summary>
        /// A tulajdonság érték növekménye
        /// </summary>
        public int Value => 1;

        public override string ToString()
        {
            return $"{this.Definition.Code} +{this.Value}";
        }
    }
}
