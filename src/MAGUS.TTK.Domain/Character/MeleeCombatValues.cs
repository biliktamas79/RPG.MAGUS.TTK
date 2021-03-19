using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter közelharci harcértékek egy konkrét fegyvernemben
    /// </summary>
    public class MeleeCombatValues
    {
        /// <summary>
        /// Fegyver definíció
        /// </summary>
        public WeaponDefinition Weapon { get; private set; }
        /// <summary>
        /// Fegyveres képzettség foka
        /// </summary>
        public MagusTtkCharacterSkillLevelsEnum SkillLevel { get; set; }

        public WeaponRangeTypesEnum RangeType = WeaponRangeTypesEnum.Melee;

        /// <summary>
        /// Tám/kör
        /// </summary>
        public int MaxAttacksPerRound { get; set; }
        /// <summary>
        /// TÉ
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
