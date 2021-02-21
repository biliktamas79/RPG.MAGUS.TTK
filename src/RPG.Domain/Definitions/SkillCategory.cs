using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Category of a skill
    /// </summary>
    public class SkillCategory : IHasUniqueCode
    {
        /// <summary>
        /// Skill category unique code, like Fight, Magic, Misc, ...
        /// </summary>
        public string Code { get; set; }
        public string Name;
    }
}
