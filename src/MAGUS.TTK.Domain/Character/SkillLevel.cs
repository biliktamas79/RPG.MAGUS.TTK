using System;
using System.Collections.Generic;
using System.Text;
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
        public StatDefinition Definition { get; private set; }

        /// <summary>
        /// A képzettség foka
        /// </summary>
        public MagusTtkCharacterSkillLevelsEnum Level { get; }
    }
}
