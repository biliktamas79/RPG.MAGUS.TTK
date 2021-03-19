using MAGUS.TTK.Data.Repositories.Json;
using MAGUS.TTK.Domain.Character;
using MAGUS.TTK.Domain.Definitions;
using Microsoft.Extensions.DependencyInjection;
using RPG.Domain;
using RPG.Domain.Character;
using RPG.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MAGUS.TTK.Data
{
    public class MagusTtkContextServiceInitializer
    {
        public void Initialize(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MagusTtkContext>()
                .AddSingleton<IReadOnlyRepository<AbilityDefinition>, InMemoryRepository<AbilityDefinition>>(serviceProvider => InitializeInMemoryRepositoryFromJson<AbilityDefinition>("abilities.json"))
                .AddSingleton<IReadOnlyRepository<SkillCategory>, InMemoryRepository<SkillCategory>>(serviceProvider => InitializeInMemoryRepositoryFromJson<SkillCategory>("skillCategories.json"))
                .AddSingleton<IReadOnlyRepository<MagusTtkSkillClassDefinition>, InMemoryRepository<MagusTtkSkillClassDefinition>>(serviceProvider => InitializeInMemoryRepositoryFromJson<MagusTtkSkillClassDefinition>("skillClasses.json"))
                .AddSingleton<IReadOnlyRepository<MagusTtkSkillDefinition>, InMemoryRepository<MagusTtkSkillDefinition>>(serviceProvider => InitializeSkillDefinitionInMemoryRepositoryFromJson(serviceProvider, "skills.json"))
                .AddSingleton<IReadOnlyRepository<WeaponCategory>, InMemoryRepository<WeaponCategory>>(serviceProvider => InitializeInMemoryRepositoryFromJson<WeaponCategory>("weaponCategories.json"))
                .AddSingleton<IReadOnlyRepository<TraitDefinition>, InMemoryRepository<TraitDefinition>>(serviceProvider => InitializeTraitDefinitionInMemoryRepositoryFromJson(serviceProvider, "traits.json"))
                .AddSingleton<IReadOnlyRepository<TalentDefinition>, InMemoryRepository<TalentDefinition>>(serviceProvider => InitializeTalentDefinitionInMemoryRepositoryFromJson(serviceProvider, "talents.json"))
                .AddSingleton<IReadOnlyRepository<Background<CodeOnlyAttribute>>, InMemoryRepository<Background<CodeOnlyAttribute>>>(serviceProvider => InitializeBackgroundDefinitionInMemoryRepositoryFromJson(serviceProvider, "origins.json"))
                .AddSingleton<IReadOnlyRepository<MagusTtkCharacterClassDefinition>, InMemoryRepository<MagusTtkCharacterClassDefinition>>(serviceProvider => InitializeCharacterClassDefinitionInMemoryRepositoryFromJson(serviceProvider, "characterClasses.json"))
                ;

            //InitializeInMemoryRepositoryInstances(serviceCollection);
        }

        //private void InitializeInMemoryRepositoryInstances(ServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddSingleton<InMemoryRepository<AbilityDefinition>>(serviceProvider => InitializeInMemoryRepositoryFromJson<AbilityDefinition>("abilities.json"));
        //    serviceCollection.AddSingleton<InMemoryRepository<SkillCategory>>(serviceProvider => InitializeInMemoryRepositoryFromJson<SkillCategory>("skillCategories.json"));
        //    serviceCollection.AddSingleton<InMemoryRepository<MagusTtkSkillClassDefinition>>(serviceProvider => InitializeInMemoryRepositoryFromJson<MagusTtkSkillClassDefinition>("skillClasses.json"));
        //    serviceCollection.AddSingleton<InMemoryRepository<MagusTtkSkillDefinition>>(serviceProvider => InitializeSkillDefinitionInMemoryRepositoryFromJson(serviceProvider, "skills.json"));
        //    serviceCollection.AddSingleton<InMemoryRepository<WeaponCategory>>(serviceProvider => InitializeInMemoryRepositoryFromJson<WeaponCategory>("weaponCategories.json"));
        //    serviceCollection.AddSingleton<InMemoryRepository<TraitDefinition>>(serviceProvider => InitializeInMemoryRepositoryFromJson<TraitDefinition>("traits.json"));
        //    serviceCollection.AddSingleton<InMemoryRepository<Background<CodeOnlyAttribute>>>(serviceProvider => InitializeInMemoryRepositoryFromJson<Background<CodeOnlyAttribute>>("origins.json"));
        //}

        private InMemoryRepository<TEntity> InitializeInMemoryRepositoryFromJson<TEntity>(string jsonFileName)
            where TEntity : IHasUniqueCode
        {
            string jsonString = File.ReadAllText(Path.Combine("Definitions", jsonFileName));
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFileName}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<TEntity[]>(jsonString);

            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(TEntity)}' from Json file '{jsonFileName}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Unique code of '{typeof(TEntity)}' in file '{jsonFileName}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Code '{entity.Code}' in file '{jsonFileName}' is a duplicate.");
            }

            var repo = new InMemoryRepository<TEntity>();
            repo.Init(entitiesFromJson);

            return repo;
        }

        private InMemoryRepository<TraitDefinition> InitializeTraitDefinitionInMemoryRepositoryFromJson(IServiceProvider serviceProvider, string jsonFileName)
        {
            string jsonString = File.ReadAllText(Path.Combine("Definitions", jsonFileName));
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFileName}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<TraitDefinition[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(TraitDefinition)}' from Json file '{jsonFileName}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            List<TraitDefinition> traitDefinitions = new List<TraitDefinition>(entitiesFromJson.Length);
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Skill code in file '{jsonFileName}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal skill definition
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Skill code '{entity.Code}' in file '{jsonFileName}' is a duplicate.");

                traitDefinitions.Add(new TraitDefinition()
                {
                    Code = entity.Code,
                    MaxValue = 3,
                    MinValue = 1
                });
            }

            var repo = new InMemoryRepository<TraitDefinition>();
            repo.Init(traitDefinitions);

            return repo;
        }

        private InMemoryRepository<TalentDefinition> InitializeTalentDefinitionInMemoryRepositoryFromJson(IServiceProvider serviceProvider, string jsonFileName)
        {
            string jsonString = File.ReadAllText(Path.Combine("Definitions", jsonFileName));
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFileName}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<TalentDefinition[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(TalentDefinition)}' from Json file '{jsonFileName}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            List<TalentDefinition> talentDefinitions = new List<TalentDefinition>(entitiesFromJson.Length);
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Skill code in file '{jsonFileName}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal skill definition
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Skill code '{entity.Code}' in file '{jsonFileName}' is a duplicate.");

                talentDefinitions.Add(new TalentDefinition()
                {
                    Code = entity.Code,
                });
            }

            var repo = new InMemoryRepository<TalentDefinition>();
            repo.Init(talentDefinitions);

            return repo;
        }

        private InMemoryRepository<MagusTtkSkillDefinition> InitializeSkillDefinitionInMemoryRepositoryFromJson(IServiceProvider serviceProvider, string jsonFileName)
        {
            var skillCategoryRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<SkillCategory>>();
            var abilityRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<AbilityDefinition>>();
            var skillClassRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<MagusTtkSkillClassDefinition>>();

            string jsonString = File.ReadAllText(Path.Combine("Definitions", jsonFileName));
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFileName}' not found.");

            // külön DTO class-ba parse-oljuk a JSON-ból
            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<MagusTtkSkillDefinitionDto[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(MagusTtkSkillDefinitionDto)}' from Json file '{jsonFileName}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            List<MagusTtkSkillDefinition> skillDefinitions = new List<MagusTtkSkillDefinition>(entitiesFromJson.Length);
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Skill code in file '{jsonFileName}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal skill definition
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Skill code '{entity.Code}' in file '{jsonFileName}' is a duplicate.");

                skillDefinitions.Add(new MagusTtkSkillDefinition()
                {
                    Code = entity.Code,
                    AbilityBase = GetValidatedSkillAbilityBase(entity, abilityRepo),
                    AbilityBaseDependsOnSpecialization = entity.AbilityBaseDependsOnSpecialization,
                    IsSecret = entity.IsSecret,
                    RequiresSpecialization = entity.RequiresSpecialization,
                    SupportsUniqueSpecialization = entity.SupportsUniqueSpecialization,
                    Specializations = GetValidatedSkillSpecializations(entity),
                    Category = GetValidatedSkillCategory(entity, skillCategoryRepo),
                    SkillClassDefinition = GetValidatedSkillClassDefinition(entity, skillClassRepo)
                });
            }

            var repo = new InMemoryRepository<MagusTtkSkillDefinition>();
            repo.Init(skillDefinitions);

            return repo;
        }

        private InMemoryRepository<Background<CodeOnlyAttribute>> InitializeBackgroundDefinitionInMemoryRepositoryFromJson(IServiceProvider serviceProvider, string jsonFileName)
        {
            var skillDefinitionRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<MagusTtkSkillDefinition>>();

            string jsonString = File.ReadAllText(Path.Combine("Definitions", jsonFileName));
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFileName}' not found.");

            // külön DTO class-ba parse-oljuk a JSON-ból
            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<Background<CodeOnlyAttribute>[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(Background<CodeOnlyAttribute>)}' from Json file '{jsonFileName}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            List<Background<CodeOnlyAttribute>> backgroundDefinitions = new List<Background<CodeOnlyAttribute>>(entitiesFromJson.Length);
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Background code in file '{jsonFileName}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Background code '{entity.Code}' in file '{jsonFileName}' is a duplicate.");

                backgroundDefinitions.Add(new Background<CodeOnlyAttribute>()
                {
                    Code = entity.Code,
                    Name = entity.Name,
                    Advantages = GetValidatedSkillCodeReferences(entity, entity.Advantages, skillDefinitionRepo),
                    Disadvantages = GetValidatedSkillCodeReferences(entity, entity.Disadvantages, skillDefinitionRepo)
                });
            }

            var repo = new InMemoryRepository<Background<CodeOnlyAttribute>>();
            repo.Init(backgroundDefinitions);

            return repo;
        }

        private InMemoryRepository<MagusTtkCharacterClassDefinition> InitializeCharacterClassDefinitionInMemoryRepositoryFromJson(IServiceProvider serviceProvider, string jsonFileName)
        {
            var abilityRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<AbilityDefinition>>();
            var skillDefinitionRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<MagusTtkSkillDefinition>>();
            var talentDefinitionRepo = serviceProvider.GetRequiredService<IReadOnlyRepository<TalentDefinition>>();

            string jsonString = File.ReadAllText(Path.Combine("Definitions", jsonFileName));
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFileName}' not found.");

            // külön DTO class-ba parse-oljuk a JSON-ból
            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<MagusTtkCharacterClassDefinitionDto[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(MagusTtkCharacterClassDefinitionDto)}' from Json file '{jsonFileName}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            List<MagusTtkCharacterClassDefinition> characterClassDefinitions = new List<MagusTtkCharacterClassDefinition>(entitiesFromJson.Length);
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Character class code in file '{jsonFileName}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Character class code '{entity.Code}' in file '{jsonFileName}' is a duplicate.");

                ValidateCharacterClassAbilities(entity, entity.Abilities, abilityRepo);
                ValidateCharacterClassLevelUpBonus(entity, entity.LevelUpBonus);

                // ha meg van adva, hogy melyik másik kasztból származik ez a kaszt definíció
                if (!string.IsNullOrWhiteSpace(entity.ParentClassCode))
                {
                    // de a hivatkozott kaszt nem ismert
                    if (!codeHashes.Contains(entity.ParentClassCode))
                        throw new ArgumentException($"ParentClassCode '{entity.ParentClassCode}' of character class code '{entity.Code}' in file '{jsonFileName}' is unknown.");
                }

                characterClassDefinitions.Add(new MagusTtkCharacterClassDefinition()
                {
                    Code = entity.Code,
                    Name = entity.Name,
                    Abilities = entity.Abilities,
                    LevelUpBonus = entity.LevelUpBonus,
                    ParentClassCode = entity.ParentClassCode,
                    Skills = GetValidatedCharacterClassSkills(entity, entity.Skills, skillDefinitionRepo),
                    Talents = GetValidatedCharacterClassTalents(entity, entity.Talents, talentDefinitionRepo)
                });
            }

            var repo = new InMemoryRepository<MagusTtkCharacterClassDefinition>();
            repo.Init(characterClassDefinitions);

            return repo;
        }

        private string[] GetValidatedSkillAbilityBase(MagusTtkSkillDefinitionDto skillDefinition, IReadOnlyRepository<AbilityDefinition> abilityRepo)
        {
            if (skillDefinition == null)
                throw new ArgumentNullException(nameof(skillDefinition));
            if (abilityRepo == null)
                throw new ArgumentNullException(nameof(abilityRepo));

            // ha nem lenne egy ability-hez sem hozzárendelve a skill (elvileg nincsen TTK-ban ilyen)
            if ((skillDefinition.AbilityBase == null) || (skillDefinition.AbilityBase.Length == 0))
            {
                // és nincs megjelölve, hogy a skill specializációjától függne
                if (!skillDefinition.AbilityBaseDependsOnSpecialization)
                    throw new ArgumentException($"AbilityBase of skill definition '{skillDefinition.Code}' is empty.");
            }

            if (skillDefinition.AbilityBase != null)
            {
                foreach (var abilityCode in skillDefinition.AbilityBase)
                {
                    // ha nem létező ability-re hivatkozna a skill
                    if (!abilityRepo.TryGetByCode(abilityCode, out var abilityDefinition))
                        throw new ArgumentException($"Ability not found with code '{abilityCode}' referenced by skill definition '{skillDefinition.Code}'.");
                }
            }

            return skillDefinition.AbilityBase;
        }

        private string[] GetValidatedSkillSpecializations(MagusTtkSkillDefinitionDto skillDefinition)
        {
            if (skillDefinition == null)
                throw new ArgumentNullException(nameof(skillDefinition));

            if (skillDefinition.Specializations != null)
            {
                foreach (var spec in skillDefinition.Specializations)
                {
                    if (string.IsNullOrWhiteSpace(spec))
                        throw new ArgumentException($"Empty specialization referenced by skill definition '{skillDefinition.Code}'.");
                }
            }

            return skillDefinition.Specializations;
        }

        private SkillCategory GetValidatedSkillCategory(MagusTtkSkillDefinitionDto skillDefinition, IReadOnlyRepository<SkillCategory> skillCategoryRepo)
        {
            if (skillDefinition == null)
                throw new ArgumentNullException(nameof(skillDefinition));
            if (skillCategoryRepo == null)
                throw new ArgumentNullException(nameof(skillCategoryRepo));

            // ha nem lenne CategoryCode-ja
            if (string.IsNullOrWhiteSpace(skillDefinition.CategoryCode))
                throw new ArgumentException($"CategoryCode of skill definition '{skillDefinition.Code}' is empty.");

            // ha nem létező SkillCategory-ra hivatkozna a skill
            if (!skillCategoryRepo.TryGetByCode(skillDefinition.CategoryCode, out var skillCategory))
                throw new ArgumentException($"Skill category not found with code '{skillDefinition.CategoryCode}' referenced by skill definition '{skillDefinition.Code}'.");

            return skillCategory;
        }

        private MagusTtkSkillClassDefinition GetValidatedSkillClassDefinition(MagusTtkSkillDefinitionDto skillDefinition, IReadOnlyRepository<MagusTtkSkillClassDefinition> skillClassDefinitionRepo)
        {
            if (skillDefinition == null)
                throw new ArgumentNullException(nameof(skillDefinition));
            if (skillClassDefinitionRepo == null)
                throw new ArgumentNullException(nameof(skillClassDefinitionRepo));

            // ha nem lenne ClassCode-ja
            if (string.IsNullOrWhiteSpace(skillDefinition.ClassCode))
                throw new ArgumentException($"ClassCode of skill definition '{skillDefinition.Code}' is empty.");

            // ha nem létező SkillClass-ra hivatkozna a skill
            if (!skillClassDefinitionRepo.TryGetByCode(skillDefinition.ClassCode, out var skillClass))
                throw new ArgumentException($"Skill class not found with code '{skillDefinition.ClassCode}' referenced by skill definition '{skillDefinition.Code}'.");

            return skillClass;
        }

        private CodeOnlyAttribute[] GetValidatedSkillCodeReferences(Background<CodeOnlyAttribute> backgroundDefinition, CodeOnlyAttribute[] skillCodeReferences, IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitionRepo)
        {
            if (backgroundDefinition == null)
                throw new ArgumentNullException(nameof(backgroundDefinition));
            if (skillCodeReferences == null)
                throw new ArgumentNullException(nameof(skillCodeReferences)); 
            if (skillDefinitionRepo == null)
                throw new ArgumentNullException(nameof(skillDefinitionRepo));

            // ha nem lenne egy skill-hez sem hozzárendelve
            if ((skillCodeReferences == null) || (skillCodeReferences.Length == 0))
                throw new ArgumentException($"Skill reference of background definition '{backgroundDefinition.Code}' is empty.");

            foreach (var skillCodeRef in skillCodeReferences)
            {
                // ha nem létező skill-re hivatkozna a skill
                if (!skillDefinitionRepo.TryGetByCode(skillCodeRef.Code, out var skillDefinition))
                {
                    int openBraceIndex = skillCodeRef.Code.IndexOf('(');
                    // ha van benne (
                    if (openBraceIndex > -1)
                    {
                        var code = skillCodeRef.Code.Substring(0, openBraceIndex).Trim();
                        // ha így már megtaláljuk a skill-t
                        if (skillDefinitionRepo.TryGetByCode(code, out skillDefinition))
                            continue;
                    }
                    throw new ArgumentException($"Skill not found with code '{skillCodeRef.Code}' referenced by background definition '{backgroundDefinition.Code}'.");
                }
            }

            return skillCodeReferences;
        }

        private CodeOnlyAttribute[] GetValidatedTalentCodeReferences(Background<CodeOnlyAttribute> backgroundDefinition, CodeOnlyAttribute[] skillCodeReferences, IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitionRepo)
        {
            if (backgroundDefinition == null)
                throw new ArgumentNullException(nameof(backgroundDefinition));
            if (skillCodeReferences == null)
                throw new ArgumentNullException(nameof(skillCodeReferences));
            if (skillDefinitionRepo == null)
                throw new ArgumentNullException(nameof(skillDefinitionRepo));

            // ha nem lenne egy skill-hez sem hozzárendelve
            if ((skillCodeReferences == null) || (skillCodeReferences.Length == 0))
                throw new ArgumentException($"Skill reference of background definition '{backgroundDefinition.Code}' is empty.");

            foreach (var skillCodeRef in skillCodeReferences)
            {
                // ha nem létező skill-re hivatkozna a skill
                if (!skillDefinitionRepo.TryGetByCode(skillCodeRef.Code, out var skillDefinition))
                {
                    int openBraceIndex = skillCodeRef.Code.IndexOf('(');
                    // ha van benne (
                    if (openBraceIndex > -1)
                    {
                        var code = skillCodeRef.Code.Substring(0, openBraceIndex).Trim();
                        // ha így már megtaláljuk a skill-t
                        if (skillDefinitionRepo.TryGetByCode(code, out skillDefinition))
                            continue;
                    }
                    throw new ArgumentException($"Skill not found with code '{skillCodeRef.Code}' referenced by background definition '{backgroundDefinition.Code}'.");
                }
            }

            return skillCodeReferences;
        }

        private void ValidateCharacterClassAbilities(MagusTtkCharacterClassDefinitionDto characterClassDefinition, Dictionary<string, int> classAbilities, IReadOnlyRepository<AbilityDefinition> abilityRepo)
        {
            if (characterClassDefinition == null)
                throw new ArgumentNullException(nameof(characterClassDefinition));
            if (classAbilities == null)
                throw new ArgumentNullException(nameof(classAbilities));
            if (abilityRepo == null)
                throw new ArgumentNullException(nameof(abilityRepo));

            int expectedAbilityCount = abilityRepo.Count();

            // ha nem a várt darabszámú tulajdonság alap érték lenne megadva
            if (classAbilities.Count != expectedAbilityCount)
                throw new ArgumentException($"Character class definition '{characterClassDefinition.Code}' has {classAbilities.Count} ability base value definitions, but should have {expectedAbilityCount}.");

            int abilityValueSum = 0;
            // leellenőrizzük, h minden tulajdonságra legyen megadva a kaszt specifikus alap érték
            foreach (var ability in abilityRepo.List())
            {
                if (!classAbilities.TryGetValue(ability.Code, out var classAbilityBaseValue))
                    throw new ArgumentException($"Ability base of '{ability.Code}' is missing from character class definition '{characterClassDefinition.Code}'.");
                
                if ((classAbilityBaseValue < 6) || (classAbilityBaseValue > 10))
                    throw new ArgumentOutOfRangeException($"Value '{classAbilityBaseValue}' of class ability '{ability.Code}' in character class definition '{characterClassDefinition.Code}' is out of range. Valid values are > 5 and < 11.");

                abilityValueSum += classAbilityBaseValue;
            }

            // leellenőrizzük, hogy a class definícióban tényleg 80 tulajdonság pontot osztottak-e szét
            if (abilityValueSum != 80)
                throw new ArgumentOutOfRangeException($"Sum of class ability base values '{abilityValueSum}' in character class definition '{characterClassDefinition.Code}' must equal to 80.");
        }

        private void ValidateCharacterClassLevelUpBonus(MagusTtkCharacterClassDefinitionDto characterClassDefinition, MagusTtkCharacterClassLevelUpStats levelUpBonus)
        {
            if (levelUpBonus == null)
                throw new ArgumentNullException(nameof(levelUpBonus));

            if ((levelUpBonus.FreeHm < 1) || (levelUpBonus.FreeHm > 3))
                throw new ArgumentException($"FreeHm value '{levelUpBonus.FreeHm}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 1-3.");
            if ((levelUpBonus.TE < 0) || (levelUpBonus.TE > 3))
                throw new ArgumentException($"TE value '{levelUpBonus.TE}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 0-3.");
            if ((levelUpBonus.VE < 0) || (levelUpBonus.VE > 3))
                throw new ArgumentException($"VE value '{levelUpBonus.VE}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 0-3.");
            if ((levelUpBonus.CE < 0) || (levelUpBonus.CE > 3))
                throw new ArgumentException($"CE value '{levelUpBonus.CE}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 0-3.");
            if ((levelUpBonus.KE < 0) || (levelUpBonus.KE > 3))
                throw new ArgumentException($"KE value '{levelUpBonus.KE}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 0-3.");
            if ((levelUpBonus.Fp < 1) || (levelUpBonus.Fp > 3))
                throw new ArgumentException($"Fp value '{levelUpBonus.Fp}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 1-3.");
            if ((levelUpBonus.Kp < 2) || (levelUpBonus.Kp > 10))
                throw new ArgumentException($"Kp value '{levelUpBonus.Kp}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 2-10.");
            if ((levelUpBonus.Pp < 0) || (levelUpBonus.Pp > 5))
                throw new ArgumentException($"Pp value '{levelUpBonus.Pp}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 0-5.");
            if ((levelUpBonus.Mp < 0) || (levelUpBonus.Mp > 5))
                throw new ArgumentException($"Mp value '{levelUpBonus.Mp}' in level up bonus of character class '{characterClassDefinition.Code}' is out of range. Valid values are 0-5.");
            if (levelUpBonus.Sp != 15)
                throw new ArgumentException($"Sp value '{levelUpBonus.Sp}' in level up bonus of character class '{characterClassDefinition.Code}' must equal to 15.");

            var sumSpCost = levelUpBonus.CalculateSpCostSum();
            if (sumSpCost != 45)
                throw new ArgumentException($"Sum Sp cost '{sumSpCost}' of level up bonus of character class '{characterClassDefinition.Code}' must equal to 45.");
        }

        private List<TalentDefinition> GetValidatedCharacterClassTalents(MagusTtkCharacterClassDefinitionDto characterClassDefinition, List<TalentDefinition> classTalents, IReadOnlyRepository<TalentDefinition> talentDefinitionRepo)
        {
            if (characterClassDefinition == null)
                throw new ArgumentNullException(nameof(characterClassDefinition));
            if (classTalents == null)
                throw new ArgumentNullException(nameof(classTalents));
            if (talentDefinitionRepo == null)
                throw new ArgumentNullException(nameof(talentDefinitionRepo));

            // ha nem a várt darabszámú adottság lenne megadva
            if (classTalents.Count != 4)
                throw new ArgumentException($"Character class definition '{characterClassDefinition.Code}' has {classTalents.Count} talents, but should have 4.");

            HashSet<string> codeHashes = new HashSet<string>();
            List<TalentDefinition> talentDefinitionList = new List<TalentDefinition>(classTalents.Count);
            foreach (var classTalent in classTalents)
            {
                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(classTalent.Code))
                    throw new ArgumentException($"Talent '{classTalent.Code}' of character class definition '{characterClassDefinition.Code}' is a duplicate.");

                // ha nem létezik ilyen kódú adottság
                if (!talentDefinitionRepo.TryGetByCode(classTalent.Code, out var talentDefinition))
                    throw new ArgumentException($"Talent '{classTalent.Code}' in character class definition '{characterClassDefinition.Code}' is unknown.");

                talentDefinitionList.Add(talentDefinition);
            }

            return talentDefinitionList;
        }

        private MagusTtkCharacterSkills GetValidatedCharacterClassSkills(MagusTtkCharacterClassDefinitionDto characterClassDefinition, SkillLevelDto[] classSkills, IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitionRepo)
        {
            if (characterClassDefinition == null)
                throw new ArgumentNullException(nameof(characterClassDefinition));
            if (classSkills == null)
                throw new ArgumentNullException(nameof(classSkills));
            if (skillDefinitionRepo == null)
                throw new ArgumentNullException(nameof(skillDefinitionRepo));

            //HashSet<string> codeHashes = new HashSet<string>();
            List<SkillLevel> skillLevelList = new List<SkillLevel>(classSkills.Length);
            foreach (var classSkill in classSkills)
            {
                // karakter kaszt definíciónál engedélyezett, h egy specializációt támogató képzettség (mintpl. Fegyverhasználat, vagy Nyelvisemeret, ...) többször is hozzá legyen adva
                //// ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                //if (!codeHashes.Add(kvp.Code))
                //    throw new ArgumentException($"Skill '{kvp.Key}' of character class definition '{characterClassDefinition.Code}' is a duplicate.");

                // ha nem létezik ilyen kódú képzettség
                if (!skillDefinitionRepo.TryGetByCode(classSkill.Code, out var skillDefinition))
                {
                    // lehet, hogy specializált képzettség, úgyhogy megpróbáljuk aszerint parse-olni
                    int firstIndex = classSkill.Code.IndexOf('(');
                    if (firstIndex < 0)
                        throw new ArgumentException($"Skill '{classSkill.Code}' in character class definition '{characterClassDefinition.Code}' is unknown.");

                    var key = classSkill.Code.Substring(0, firstIndex).Trim();
                    var spec = classSkill.Code.Substring(firstIndex + 1).Trim().TrimEnd(')');

                    if (!skillDefinitionRepo.TryGetByCode(key, out skillDefinition))
                        throw new ArgumentException($"Skill '{key}' in character class definition '{characterClassDefinition.Code}' is unknown.");

                    if (!skillDefinition.RequiresSpecialization)
                        throw new ArgumentException($"Skill '{key}' was found with specialization '{spec}' in character class definition '{characterClassDefinition.Code}', but skill does not require specialization according to its definition.");

                    // if the specialization is not valid
                    if (!skillDefinition.SupportsUniqueSpecialization && !skillDefinition.Specializations.Contains(spec))
                        throw new ArgumentException($"Specialization '{spec}' of skill '{key}' in character class definition '{characterClassDefinition.Code}' is invalid.");

                    classSkill.Specialization = spec;
                }

                // ha van specializáció megadva
                if (!string.IsNullOrWhiteSpace(classSkill.Specialization))
                {
                    // de a skill nem specializálható
                    if (!skillDefinition.RequiresSpecialization)
                        throw new ArgumentException($"Skill '{classSkill.Code}' was found with specialization '{classSkill.Specialization}' in character class definition '{characterClassDefinition.Code}', but skill does not require specialization according to its definition.");

                    // if the specialization is not valid
                    if (!skillDefinition.SupportsUniqueSpecialization && !skillDefinition.Specializations.Contains(classSkill.Specialization))
                        throw new ArgumentException($"Specialization '{classSkill.Specialization}' of skill '{classSkill.Code}' in character class definition '{characterClassDefinition.Code}' is invalid.");
                }

                //if (!Enum.TryParse<MagusTtkCharacterSkillLevelsEnum>(classSkill.Level, out var enumValue))
                //    throw new ArgumentException($"Could not parse '{classSkill.Level}' as a {nameof(MagusTtkCharacterSkillLevelsEnum)} value in skill '{classSkill.Code}' in character class definition '{characterClassDefinition.Code}'.");

                skillLevelList.Add(new SkillLevel()
                {
                    Definition = skillDefinition,
                    Level = classSkill.Level,
                    Specialization = classSkill.Specialization
                });
            }

            var skills = new MagusTtkCharacterSkills();
            skills.Initialize(skillLevelList);
            return skills;
        }
    }
}
