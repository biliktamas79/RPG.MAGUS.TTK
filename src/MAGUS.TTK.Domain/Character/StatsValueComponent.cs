﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter statisztika, mint KÉ, TÉ, VÉ, CÉ, Ép, Fp, Kp, Pp, Mp, Sp, ...
    /// </summary>
    public class StatsValueComponent
    {
        [JsonIgnoreAttribute]
        public readonly StatDefinition Definition;
        public readonly StatsValueComponentTypeEnum Type;
        public int Value;
    }
}
