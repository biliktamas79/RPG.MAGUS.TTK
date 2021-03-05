using MAGUS.TTK.Domain.Character;
using RPG.Domain;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Definition of a skill in MAGUS TTK
    /// </summary>
    public class MagusTtkSkillClassDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Skill class unique code, like I., II., III., IV., V.
        /// </summary>
        public string Code { get; set; }
        public int[] KpPrice { get; set; }
    }
}
