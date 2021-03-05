using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of a skill
    /// </summary>
    public class SkillDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Skill unique code, like Fight.Melee, ...
        /// </summary>
        public string Code { get; set; }
        public SkillCategory Category { get; set; }
    }
}
