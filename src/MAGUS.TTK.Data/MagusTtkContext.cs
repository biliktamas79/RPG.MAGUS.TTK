using MAGUS.TTK.Domain.Character;
using MAGUS.TTK.Domain.Definitions;
using RPG.Domain;
using RPG.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Data
{
    public class MagusTtkContext
    {
        public MagusTtkContext(
            IReadOnlyRepository<AbilityDefinition> abilityDefinitions,
            IReadOnlyRepository<SkillCategory> skillCategoryDefinitions,
            IReadOnlyRepository<MagusTtkSkillClassDefinition> skillClassDefinitions,
            IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitions,
            IReadOnlyRepository<WeaponCategory> weaponCategoryDefinitions,
            IReadOnlyRepository<TraitDefinition> traitDefinitions,
            IReadOnlyRepository<TalentDefinition> talentDefinitions,
            IReadOnlyRepository<Background<CodeOnlyAttribute>> originDefinitions,
            IReadOnlyRepository<MagusTtkCharacterClassDefinition> characterClassDefinitions,
            IReadOnlyRepository<MagusTtkWeaponDefinition> weaponDefinitions)
        {
            this.AbilityDefinitions = abilityDefinitions ?? throw new ArgumentNullException(nameof(abilityDefinitions));
            this.SkillCategoryDefinitions = skillCategoryDefinitions ?? throw new ArgumentNullException(nameof(skillCategoryDefinitions));
            this.SkillClassDefinitions = skillClassDefinitions ?? throw new ArgumentNullException(nameof(skillClassDefinitions));
            this.SkillDefinitions = skillDefinitions ?? throw new ArgumentNullException(nameof(skillDefinitions));
            this.WeaponCategoryDefinitions = weaponCategoryDefinitions ?? throw new ArgumentNullException(nameof(weaponCategoryDefinitions));
            this.TraitDefinitions = traitDefinitions ?? throw new ArgumentNullException(nameof(traitDefinitions));
            this.TalentDefinitions = talentDefinitions ?? throw new ArgumentNullException(nameof(talentDefinitions));
            this.OriginDefinitions = originDefinitions ?? throw new ArgumentNullException(nameof(originDefinitions));
            this.CharacterClassDefinitions = characterClassDefinitions ?? throw new ArgumentNullException(nameof(characterClassDefinitions));
            this.WeaponDefinitions = weaponDefinitions ?? throw new ArgumentNullException(nameof(weaponDefinitions));
        }

        public IReadOnlyRepository<AbilityDefinition> AbilityDefinitions { get; private set; }
        public IReadOnlyRepository<SkillCategory> SkillCategoryDefinitions { get; private set; }
        public IReadOnlyRepository<MagusTtkSkillClassDefinition> SkillClassDefinitions { get; private set; }
        public IReadOnlyRepository<MagusTtkSkillDefinition> SkillDefinitions { get; private set; }
        public IReadOnlyRepository<WeaponCategory> WeaponCategoryDefinitions { get; private set; }
        public IReadOnlyRepository<TraitDefinition> TraitDefinitions { get; private set; }
        public IReadOnlyRepository<TalentDefinition> TalentDefinitions { get; private set; }
        public IReadOnlyRepository<Background<CodeOnlyAttribute>> OriginDefinitions { get; private set; }
        public IReadOnlyRepository<MagusTtkCharacterClassDefinition> CharacterClassDefinitions { get; private set; }
        public IReadOnlyRepository<MagusTtkWeaponDefinition> WeaponDefinitions { get; private set; }

        internal bool IsDataInitialized;
    }
}
