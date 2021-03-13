using MAGUS.TTK.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Definition of a talent in MAGUS TTK
    /// </summary>
    public class MagusTtkTalentDefinition : TalentDefinition
    {
        public string Description;
        ///// <summary>
        ///// Abilities this skill is based on. Ability requirements for reaching certain levels of this skill are based on these.
        ///// </summary>
        //public MagusTtkTalentLevelDefinition[] Levels { get; set; }
    }
}
