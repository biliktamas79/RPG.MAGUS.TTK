﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonIgnoreAttribute]
        public AbilityDefinition Definition { get; set; }

        /// <summary>
        /// A tulajdonság értéke
        /// </summary>
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{this.Definition.Code}: {this.Value}";
        }
    }
}
