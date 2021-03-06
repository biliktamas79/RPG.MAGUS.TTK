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
                .AddSingleton<IReadOnlyRepository<Background<CodeOnlyAttribute>>, InMemoryRepository<Background<CodeOnlyAttribute>>>(serviceProvider => InitializeBackgroundDefinitionInMemoryRepositoryFromJson(serviceProvider, "origins.json"))
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
    }
}
