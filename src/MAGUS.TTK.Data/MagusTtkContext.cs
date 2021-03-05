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
            IReadOnlyRepository<Background<CodeOnlyAttribute>> originDefinitions)
        {
            this.AbilityDefinitions = abilityDefinitions ?? throw new ArgumentNullException(nameof(abilityDefinitions));
            this.SkillCategoryDefinitions = skillCategoryDefinitions ?? throw new ArgumentNullException(nameof(skillCategoryDefinitions));
            this.SkillClassDefinitions = skillClassDefinitions ?? throw new ArgumentNullException(nameof(skillClassDefinitions));
            this.SkillDefinitions = skillDefinitions ?? throw new ArgumentNullException(nameof(skillDefinitions));
            this.WeaponCategoryDefinitions = weaponCategoryDefinitions ?? throw new ArgumentNullException(nameof(weaponCategoryDefinitions));
            this.TraitDefinitions = traitDefinitions ?? throw new ArgumentNullException(nameof(traitDefinitions));
            this.OriginDefinitions = originDefinitions ?? throw new ArgumentNullException(nameof(originDefinitions));
        }

        public IReadOnlyRepository<AbilityDefinition> AbilityDefinitions { get; private set; }
        public IReadOnlyRepository<SkillCategory> SkillCategoryDefinitions { get; private set; }
        public IReadOnlyRepository<MagusTtkSkillClassDefinition> SkillClassDefinitions { get; private set; }
        public IReadOnlyRepository<MagusTtkSkillDefinition> SkillDefinitions { get; private set; }
        public IReadOnlyRepository<WeaponCategory> WeaponCategoryDefinitions { get; private set; }
        public IReadOnlyRepository<TraitDefinition> TraitDefinitions { get; private set; }
        public IReadOnlyRepository<Background<CodeOnlyAttribute>> OriginDefinitions { get; private set; }
    }
}
