using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using MAGUS.TTK.Domain.Definitions;
using RPG.Domain;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képzettség szint DTO
    /// </summary>
    public class SkillLevelDto : IHasUniqueCode
    {
        /// <summary>
        /// Képzettség kód
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Képzettség specializáció
        /// </summary>
        public string Specialization { get; set; }

        /// <summary>
        /// A képzettség foka
        /// </summary>
        public MagusTtkCharacterSkillLevelsEnum Level { get; set; }
    }
}
