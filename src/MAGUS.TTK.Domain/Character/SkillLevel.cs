using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képzettség
    /// </summary>
    public class SkillLevel
    {
        /// <summary>
        /// Képzettség definíció
        /// </summary>
        [JsonIgnoreAttribute]
        public StatDefinition Definition { get; private set; }

        /// <summary>
        /// A képzettség foka
        /// </summary>
        public MagusTtkCharacterSkillLevelsEnum Level { get; }
    }
}
