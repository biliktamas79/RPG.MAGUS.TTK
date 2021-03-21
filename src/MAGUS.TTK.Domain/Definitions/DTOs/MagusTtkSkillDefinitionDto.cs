using MAGUS.TTK.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Definition of a skill in MAGUS TTK
    /// </summary>
    public class MagusTtkSkillDefinitionDto
    {
        public string Code { get; set; }
        public string Group { get; set; }
        public int DisplayOrderInGroup { get; set; }
        public string ClassCode { get; set; }
        public string CategoryCode { get; set; }
        //public string Name { get; set; }
        /// <summary>
        /// Abilities this skill is based on. Ability requirements for reaching certain levels of this skill are based on these.
        /// </summary>
        public string[] AbilityBase { get; set; }
        public bool AbilityBaseDependsOnSpecialization { get; set; }
        public bool RequiresSpecialization { get; set; }
        public bool SupportsUniqueSpecialization { get; set; }
        public string[] Specializations { get; set; }
        public bool IsSecret { get; set; }
    }
}
