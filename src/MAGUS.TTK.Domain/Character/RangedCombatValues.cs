using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter távolsági harcértékek egy konkrét fegyvernemben
    /// </summary>
    public class RangedCombatValues
    {
        /// <summary>
        /// Tulajdonság definíció
        /// </summary>
        public WeaponDefinition Weapon { get; private set; }
        /// <summary>
        /// Fegyveres képzettség foka
        /// </summary>
        public MagusTtkCharacterSkillLevelsEnum SkillLevel { get; set; }

        public WeaponRangeTypesEnum RangeType = WeaponRangeTypesEnum.Ranged;

        /// <summary>
        /// Tám/kör
        /// </summary>
        public int MaxAttacksPerRound { get; set; }
        /// <summary>
        /// CÉ
        /// </summary>
        public int Attack { get; set; }
        /// <summary>
        /// VÉ
        /// </summary>
        public int Defense { get; set; }
        /// <summary>
        /// Sebzés (pl. d5+3)
        /// </summary>
        public string DamageRollCode { get; }
    }
}
