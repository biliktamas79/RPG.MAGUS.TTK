using System;
using System.Collections.Generic;
using System.Text;
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
        public StatDefinition Definition { get; private set; }

        /// <summary>
        /// Tulajdonság módosítók típus szerint
        /// </summary>
        public readonly Dictionary<StatsValueComponentTypeEnum, StatsValueComponent> ValueComponentsByType = new Dictionary<StatsValueComponentTypeEnum, StatsValueComponent>(10);

        /// <summary>
        /// A tulajdonság értéke
        /// </summary>
        public int Value { get; }
    }
}
