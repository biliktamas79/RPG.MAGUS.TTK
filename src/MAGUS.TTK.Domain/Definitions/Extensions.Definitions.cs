using MAGUS.TTK.Domain.Character;
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
                ? skillDefs.OrderBy(skillDef => skillDef.Group).ThenBy(skillDef => skillDef.DisplayOrderInGroup).ThenBy(skillDef => skillDef.Code)
                : skillDefs;
        }

        public static IEnumerable<SkillLevel> GetSkillLevelsOfCategory(this IEnumerable<SkillLevel> skillLevels, SkillCategory category, bool orderByDefault = true)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            if (skillLevels == null)
                return Enumerable.Empty<SkillLevel>();

            var skillLvls = skillLevels.Where(skillLevel => skillLevel.Definition.Category.Code == category.Code);

            return orderByDefault
                ? skillLvls.OrderBy(skillLevel => skillLevel.Definition.Group).ThenBy(skillLevel => skillLevel.Definition.DisplayOrderInGroup).ThenBy(skillLevel => skillLevel.Definition.Code)
                : skillLvls;
        }

        public static IEnumerable<IGrouping<SkillCategory, MagusTtkSkillDefinition>> GroupSkillDefinitionsByCategory(this IEnumerable<MagusTtkSkillDefinition> skillDefinitions, bool orderByDefault = true)
        {
            if (skillDefinitions == null)
                return Enumerable.Empty<IGrouping<SkillCategory, MagusTtkSkillDefinition>>();

            if (orderByDefault)
                skillDefinitions = skillDefinitions.OrderBy(skillDef => skillDef.Group).ThenBy(skillDef => skillDef.DisplayOrderInGroup).ThenBy(skillDef => skillDef.Code);

            var groups = skillDefinitions.GroupBy(skillDef => skillDef.Category);

            return orderByDefault
                ? groups.OrderBy(group => group.Key.DisplayOrder).ThenBy(group => group.Key.Name ?? group.Key.Code)
                : groups;
        }

        public static IEnumerable<IGrouping<SkillCategory, SkillLevel>> GroupSkillLevelsByCategory(this IEnumerable<SkillLevel> skillLevels, bool orderByDefault = true)
        {
            if (skillLevels == null)
                return Enumerable.Empty<IGrouping<SkillCategory, SkillLevel>>();

            if (orderByDefault)
                skillLevels = skillLevels.OrderBy(skillLevel => skillLevel.Definition.Group).ThenBy(skillLevel => skillLevel.Definition.DisplayOrderInGroup).ThenBy(skillLevel => skillLevel.Definition.Code);

            var groups = skillLevels.GroupBy(skillLevel => skillLevel.Definition.Category);

            return orderByDefault
                ? groups.OrderBy(group => group.Key.DisplayOrder).ThenBy(group => group.Key.Name ?? group.Key.Code)
                : groups;
        }
    }
}
