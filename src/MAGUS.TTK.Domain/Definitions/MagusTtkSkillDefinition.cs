﻿using MAGUS.TTK.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Definition of a skill in MAGUS TTK
    /// </summary>
    public class MagusTtkSkillDefinition : SkillDefinition
    {
        /// <summary>
        /// Definition of one of the 5 skill classes in TTK
        /// </summary>
        public MagusTtkSkillClassDefinition SkillClassDefinition { get; set; }
        /// <summary>
        /// Abilities this skill is based on. Ability requirements for reaching certain levels of this skill are based on these.
        /// </summary>
        public string[] AbilityBase { get; set; }
        public bool AbilityBaseDependsOnSpecialization { get; set; }
        public bool RequiresSpecialization { get; set; }
        public bool SupportsUniqueSpecialization { get; set; }
        public string[] Specializations { get; set; }
    }
}