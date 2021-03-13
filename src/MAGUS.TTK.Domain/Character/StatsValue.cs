using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter statisztika, mint KÉ, TÉ, VÉ, CÉ, Ép, Fp, ...
    /// </summary>
    public class StatsValue
    {
        /// <summary>
        /// Tulajdonság definíció
        /// </summary>
        [JsonIgnoreAttribute]
        public StatDefinition Definition { get; private set; }

        /// <summary>
        /// Tulajdonság módosítók típus szerint
        /// </summary>
        public readonly Dictionary<StatsValueComponentTypeEnum, StatsValueComponent> ValueComponentsByType = new Dictionary<StatsValueComponentTypeEnum, StatsValueComponent>(10);

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
