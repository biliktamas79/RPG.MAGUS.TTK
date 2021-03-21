using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using MAGUS.TTK.Domain.Definitions;
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
        public MagusTtkSkillDefinition Definition { get; set; }

        /// <summary>
        /// A képzettség foka
        /// </summary>
        public MagusTtkCharacterSkillLevelsEnum Level { get; set; }

        /// <summary>
        /// A képzettség specializációja, mint pl. a Fegyverhasználatnál a fegyver kategória, vagy a Nyelvismeret-nél a nyelv, ...
        /// </summary>
        public string Specialization { get; set; }

        public bool RequiresSpecialization
        {
            get { return this.Definition?.RequiresSpecialization ?? false; }
        }

        /// <summary>
        /// Get the default Kp cost of this skill level.
        /// </summary>
        /// <returns></returns>
        public int GetKpCost()
        {
            return this.Definition.SkillClassDefinition.GetKpCostOfLevel(this.Level);
        }

        public override string ToString()
        {
            return this.RequiresSpecialization
                ? $"{this.Definition?.Code} ({this.Specialization}) {this.Level}"
                : $"{this.Definition?.Code} {this.Level}";
        }
    }
}
