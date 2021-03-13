using System;
using System.Collections.Generic;
using System.Text;
using MAGUS.TTK.Domain.Character;
using RPG.Domain;
using RPG.Domain.Character;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// A MAGUS TTK rendszerében egy karakter kaszt definíciója
    /// </summary>
    public class MagusTtkCharacterClassDefinitionDto : IHasUniqueCode
    {
        /// <summary>
        /// Az alap karakter kaszt amiből ez a specializált változat leszármazik
        /// </summary>
        public string ParentClassCode { get; set; }
        /// <summary>
        /// A karakter kaszt kódja
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// A karakter kaszt neve
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tulajdonságok
        /// </summary>
        public Dictionary<string, int> Abilities;
        /// <summary>
        /// Képzettségek
        /// </summary>
        public SkillLevelDto[] Skills;
        /// <summary>
        /// Kalandozói bónusz
        /// </summary>
        public MagusTtkCharacterClassLevelUpStats LevelUpBonus { get; set; }
        /// <summary>
        /// Adottságok
        /// </summary>
        public List<TalentDefinition> Talents;
    }
}
