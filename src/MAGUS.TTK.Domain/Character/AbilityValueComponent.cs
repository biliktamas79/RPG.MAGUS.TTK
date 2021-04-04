using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képesség, mint Erő, Ügy, ...
    /// </summary>
    public class AbilityValueComponent
    {
        public AbilityValueComponent(AbilityValueComponentTypeEnum type)
        {
            this.Type = type;
        }

        public readonly AbilityValueComponentTypeEnum Type;
        public int Value;
        public int? Level;
    }
}
