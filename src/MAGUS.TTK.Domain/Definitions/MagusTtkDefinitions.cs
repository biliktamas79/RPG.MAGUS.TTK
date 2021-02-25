using MAGUS.TTK.Domain.Character;
using RPG.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    public class MagusTtkDefinitions
    {
        public string Version { get; set; }

        /// <summary>
        /// Karakter Tulajdonság definíciók (Erő, Gyo, Ügy, ...)
        /// </summary>
        public readonly Dictionary<MagusTtkCharacterAbilityEnum, AbilityDefinition> CharacterAbilities = new Dictionary<MagusTtkCharacterAbilityEnum, AbilityDefinition>(10)
        {
            { MagusTtkCharacterAbilityEnum.Strength, new AbilityDefinition() { Code = "Erő", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Quickness, new AbilityDefinition() { Code = "Gyo", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Dexterity, new AbilityDefinition() { Code = "Ügy", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Stamina, new AbilityDefinition() { Code = "Áll", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Health, new AbilityDefinition() { Code = "Egé", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Charisma, new AbilityDefinition() { Code = "Kar", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Intelligence, new AbilityDefinition() { Code = "Int", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Will, new AbilityDefinition() { Code = "Aka", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Astral, new AbilityDefinition() { Code = "Asz", MinValue = 6, MaxValue = 20 } },
            { MagusTtkCharacterAbilityEnum.Perception, new AbilityDefinition() { Code = "Érz", MinValue = 6, MaxValue = 20 } },
        };

        public readonly Dictionary<MagusTtkSkillCategoryEnum, SkillCategory> SkillCategories = new Dictionary<MagusTtkSkillCategoryEnum, SkillCategory>(6)
        {
            { MagusTtkSkillCategoryEnum.Harci, new SkillCategory() { Code = "HARCI", Name = "Harci" } },
            { MagusTtkSkillCategoryEnum.Tudomanyos, new SkillCategory() { Code = "TUDOMANYOS", Name = "Tudományos" } },
            { MagusTtkSkillCategoryEnum.Szocialis, new SkillCategory() { Code = "SZOCIALIS", Name = "Szociális" } },
            { MagusTtkSkillCategoryEnum.Vilagi, new SkillCategory() { Code = "VILAGI", Name = "Világi" } },
            { MagusTtkSkillCategoryEnum.Alvilagi, new SkillCategory() { Code = "ALVILAGI", Name = "Alvilági" } },
            { MagusTtkSkillCategoryEnum.Magikus, new SkillCategory() { Code = "MAGIKUS", Name = "Mágikus" } },
        };

        public readonly Dictionary<string, WeaponCategory> WeaponCategoryDefinitions = new Dictionary<string, WeaponCategory>()
        {
            { "RP", new WeaponCategory() { Code = "RP", Name = "Rövid penge" } },
            { "HP", new WeaponCategory() { Code = "HP", Name = "Hosszú penge" } },
            { "ÓP", new WeaponCategory() { Code = "ÓP", Name = "Óriás penge" } },
            { "HAS", new WeaponCategory() { Code = "HAS", Name = "Hasító" } },
            { "ZÚZ", new WeaponCategory() { Code = "ZÚZ", Name = "Zúzó" } },
            { "SZÁ", new WeaponCategory() { Code = "SZÁ", Name = "Szálfegyver" } },
            { "LOV", new WeaponCategory() { Code = "LOV", Name = "Lovas" } },
            { "NYD", new WeaponCategory() { Code = "NYD", Name = "Nyársaló dobó" } },
            { "PD", new WeaponCategory() { Code = "PD", Name = "Pergő dobó" } },
            { "ÍJ", new WeaponCategory() { Code = "ÍJ", Name = "Íj" } },
            { "SZÍ", new WeaponCategory() { Code = "SZÍ", Name = "Számszeríj" } },
            { "EGY", new WeaponCategory() { Code = "EGY", Name = "Egyedi" } },
            { "HM", new WeaponCategory() { Code = "HM", Name = "Harcművész" } },
        };

        public readonly Dictionary<string, TraitDefinition> TraitDefinitions = new Dictionary<string, TraitDefinition>()
        {
            { "barátságos", new TraitDefinition() { Code = "barátságos", MinValue = 0, MaxValue = 5 } },
            { "befolyásolható", new TraitDefinition() { Code = "befolyásolható", MinValue = 0, MaxValue = 5 } },
        };

        public readonly Dictionary<string, Background<CodeOnlyAttribute>> Origins = new Dictionary<string, Background<CodeOnlyAttribute>>()
        {
            { "alidari", new Background<CodeOnlyAttribute>() { Code = "alidari", Name = "alidari", Advantages = new CodeOnlyAttribute[] { new CodeOnlyAttribute("Álcázás/álruha"), new CodeOnlyAttribute("Boszorkánymágia"), new CodeOnlyAttribute("Lélektan") }, Disadvantages = new CodeOnlyAttribute[] { new CodeOnlyAttribute("Lovaglás"), new CodeOnlyAttribute("Pusztítás"), new CodeOnlyAttribute("Vértviselet") } } },
            { "amazon", new Background<CodeOnlyAttribute>() { Code = "amazon", Name = "amazon", Advantages = new CodeOnlyAttribute[] { new CodeOnlyAttribute("Lélektan"), new CodeOnlyAttribute("Trükk"), new CodeOnlyAttribute("Túlélés") }, Disadvantages = new CodeOnlyAttribute[] { new CodeOnlyAttribute("Alkímia"), new CodeOnlyAttribute("Heraldika"), new CodeOnlyAttribute("Pusztítás") } } },
        };
    }
}
