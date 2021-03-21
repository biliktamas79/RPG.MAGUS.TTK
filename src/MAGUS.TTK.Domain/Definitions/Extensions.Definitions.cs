using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    public static class Extensions
    {
        public static IEnumerable<MagusTtkSkillDefinition> GetSkillDefinitionsOfCategory(this IEnumerable<MagusTtkSkillDefinition> skillDefinitions, SkillCategory category, bool orderByDefault = true)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            if (skillDefinitions == null)
                return Enumerable.Empty<MagusTtkSkillDefinition>();

            var skillDefs = skillDefinitions.Where(skillDef => skillDef.Category.Code == category.Code);

            return orderByDefault
                ? skillDefs.OrderBy(skillDef => skillDef.Code)
                : skillDefs;
        }

        public static IEnumerable<IGrouping<SkillCategory, MagusTtkSkillDefinition>> GroupSkillDefinitionsByCategory(this IEnumerable<MagusTtkSkillDefinition> skillDefinitions, bool orderByDefault = true)
        {
            if (skillDefinitions == null)
                return Enumerable.Empty<IGrouping<SkillCategory, MagusTtkSkillDefinition>>();

            if (orderByDefault)
                skillDefinitions = skillDefinitions.OrderBy(skillDef => skillDef.Code);

            var groups = skillDefinitions.GroupBy(skillDef => skillDef.Category);

            return orderByDefault
                ? groups.OrderBy(group => group.Key.DisplayOrder)
                : groups;
        }
    }
}
