using MAGUS.TTK.Data.Repositories.Json;
using MAGUS.TTK.Domain;
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
using System.Threading;
using System.Threading.Tasks;

namespace MAGUS.TTK.Data
{
    public class MagusTtkDataContextInitializer : IDataInitializer<MagusTtkContext>
    {
        private readonly IFileContentResolver fileContentResolver;

        public MagusTtkDataContextInitializer(IFileContentResolver fileContentResolver)
        {
            this.fileContentResolver = fileContentResolver ?? throw new ArgumentNullException(nameof(fileContentResolver));
        }

        private async Task InitializeInMemoryRepositoryFromJson<TEntity>(MagusTtkContext dataContext, IReadOnlyRepository<TEntity> repo, string jsonFilePath, CancellationToken cancellationToken = default)
            where TEntity : IHasUniqueCode
        {
            string jsonString = await fileContentResolver.ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<TEntity[]>(jsonString);

            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(TEntity)}' from Json file '{jsonFilePath}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            var editableRepo = repo.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Unique code of '{typeof(TEntity)}' in file '{jsonFilePath}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                await editableRepo.Add(entity, cancellationToken);
            }
        }

        private async Task InitializeTraitDefinitionInMemoryRepositoryFromJson(MagusTtkContext dataContext, string jsonFilePath, CancellationToken cancellationToken = default)
        {
            string jsonString = await fileContentResolver.ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<TraitDefinition[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(TraitDefinition)}' from Json file '{jsonFilePath}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            var editableRepo = dataContext.TraitDefinitions.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Skill code in file '{jsonFilePath}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal skill definition
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Skill code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                var newEntity =new TraitDefinition()
                {
                    Code = entity.Code,
                    MaxValue = 3,
                    MinValue = 1
                };

                await editableRepo.Add(newEntity, cancellationToken);
            }
        }

        private async Task InitializeTalentDefinitionInMemoryRepositoryFromJson(MagusTtkContext dataContext, string jsonFilePath, CancellationToken cancellationToken = default)
        {
            string jsonString = await fileContentResolver .ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<TalentDefinition[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(TalentDefinition)}' from Json file '{jsonFilePath}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            var editableRepo = dataContext.TalentDefinitions.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Skill code in file '{jsonFilePath}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal skill definition
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Skill code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                var newEntity = new TalentDefinition()
                {
                    Code = entity.Code,
                };

                await editableRepo.Add(newEntity, cancellationToken);
            }
        }

        private async Task InitializeSkillDefinitionInMemoryRepositoryFromJson(MagusTtkContext dataContext, string jsonFilePath, CancellationToken cancellationToken = default)
        {
            string jsonString = await fileContentResolver.ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            // külön DTO class-ba parse-oljuk a JSON-ból
            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<MagusTtkSkillDefinitionDto[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(MagusTtkSkillDefinitionDto)}' from Json file '{jsonFilePath}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            var editableRepo = dataContext.SkillDefinitions.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Skill code in file '{jsonFilePath}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal skill definition
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Skill code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                var newEntity = new MagusTtkSkillDefinition()
                {
                    Code = entity.Code,
                    Group = entity.Group,
                    DisplayOrderInGroup = entity.DisplayOrderInGroup,
                    AbilityBase = await GetValidatedSkillAbilityBase(entity, dataContext.AbilityDefinitions, cancellationToken),
                    AbilityBaseDependsOnSpecialization = entity.AbilityBaseDependsOnSpecialization,
                    IsSecret = entity.IsSecret,
                    RequiresSpecialization = entity.RequiresSpecialization,
                    SupportsUniqueSpecialization = entity.SupportsUniqueSpecialization,
                    Specializations = GetValidatedSkillSpecializations(entity),
                    Category = await GetValidatedSkillCategory(entity, dataContext.SkillCategoryDefinitions, cancellationToken),
                    SkillClassDefinition = await GetValidatedSkillClassDefinition(entity, dataContext.SkillClassDefinitions, cancellationToken)
                };

                await editableRepo.Add(newEntity, cancellationToken);
            }
        }

        private async Task InitializeBackgroundDefinitionInMemoryRepositoryFromJson(MagusTtkContext dataContext, string jsonFilePath, CancellationToken cancellationToken = default)
        {
            string jsonString = await fileContentResolver.ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            // külön DTO class-ba parse-oljuk a JSON-ból
            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<Background<CodeOnlyAttribute>[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(Background<CodeOnlyAttribute>)}' from Json file '{jsonFilePath}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            var editableRepo = dataContext.OriginDefinitions.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Background code in file '{jsonFilePath}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Background code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                var newEntity = new Background<CodeOnlyAttribute>()
                {
                    Code = entity.Code,
                    Name = entity.Name,
                    Advantages = await GetValidatedSkillCodeReferences(entity, entity.Advantages, dataContext.SkillDefinitions, cancellationToken),
                    Disadvantages = await GetValidatedSkillCodeReferences(entity, entity.Disadvantages, dataContext.SkillDefinitions, cancellationToken)
                };

                await editableRepo.Add(newEntity, cancellationToken);
            }
        }

        private async Task InitializeCharacterClassDefinitionInMemoryRepositoryFromJson(MagusTtkContext dataContext, string jsonFilePath, CancellationToken cancellationToken = default)
        {
            string jsonString = await fileContentResolver.ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            // külön DTO class-ba parse-oljuk a JSON-ból
            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<MagusTtkCharacterClassDefinitionDto[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(MagusTtkCharacterClassDefinitionDto)}' from Json file '{jsonFilePath}'.");

            HashSet<string> codeHashes = new HashSet<string>();
            var editableRepo = dataContext.CharacterClassDefinitions.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Character class code in file '{jsonFilePath}' is empty.");

                // ha nem tudja hozzáadni, akkor már volt ilyen kóddal
                if (!codeHashes.Add(entity.Code))
                    throw new ArgumentException($"Character class code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                await ValidateCharacterClassAbilities(entity, entity.Abilities, dataContext.AbilityDefinitions, cancellationToken);
                ValidateCharacterClassLevelUpBonus(entity, entity.LevelUpBonus);

                // ha meg van adva, hogy melyik másik kasztból származik ez a kaszt definíció
                if (!string.IsNullOrWhiteSpace(entity.ParentClassCode))
                {
                    // de a hivatkozott kaszt nem ismert
                    if (!codeHashes.Contains(entity.ParentClassCode))
                        throw new ArgumentException($"ParentClassCode '{entity.ParentClassCode}' of character class code '{entity.Code}' in file '{jsonFilePath}' is unknown.");
                }

                var newEntity = new MagusTtkCharacterClassDefinition()
                {
                    Code = entity.Code,
                    Name = entity.Name,
                    Abilities = entity.Abilities,
                    LevelUpBonus = entity.LevelUpBonus,
                    ParentClassCode = entity.ParentClassCode,
                    Skills = await GetValidatedCharacterClassSkills(entity, entity.Skills, dataContext.SkillDefinitions, cancellationToken),
                    Talents = await GetValidatedCharacterClassTalents (entity, entity.Talents, dataContext.TalentDefinitions, cancellationToken)
                };

                await editableRepo.Add(newEntity, cancellationToken);
            }
        }

        private async Task<string[]> GetValidatedSkillAbilityBase(MagusTtkSkillDefinitionDto skillDefinition, IReadOnlyRepository<AbilityDefinition> abilityRepo, CancellationToken cancellationToken = default)
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
                    if (!await abilityRepo.TryGetByCode(abilityCode, out var abilityDefinition, cancellationToken))
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

        private async Task<SkillCategory> GetValidatedSkillCategory(MagusTtkSkillDefinitionDto skillDefinition, IReadOnlyRepository<SkillCategory> skillCategoryRepo, CancellationToken cancellationToken = default)
        {
            if (skillDefinition == null)
                throw new ArgumentNullException(nameof(skillDefinition));
            if (skillCategoryRepo == null)
                throw new ArgumentNullException(nameof(skillCategoryRepo));

            // ha nem lenne CategoryCode-ja
            if (string.IsNullOrWhiteSpace(skillDefinition.CategoryCode))
                throw new ArgumentException($"CategoryCode of skill definition '{skillDefinition.Code}' is empty.");

            // ha nem létező SkillCategory-ra hivatkozna a skill
            if (!await skillCategoryRepo.TryGetByCode(skillDefinition.CategoryCode, out var skillCategory, cancellationToken))
                throw new ArgumentException($"Skill category not found with code '{skillDefinition.CategoryCode}' referenced by skill definition '{skillDefinition.Code}'.");

            return skillCategory;
        }

        private async Task<MagusTtkSkillClassDefinition> GetValidatedSkillClassDefinition(MagusTtkSkillDefinitionDto skillDefinition, IReadOnlyRepository<MagusTtkSkillClassDefinition> skillClassDefinitionRepo, CancellationToken cancellationToken = default)
        {
            if (skillDefinition == null)
                throw new ArgumentNullException(nameof(skillDefinition));
            if (skillClassDefinitionRepo == null)
                throw new ArgumentNullException(nameof(skillClassDefinitionRepo));

            // ha nem lenne ClassCode-ja
            if (string.IsNullOrWhiteSpace(skillDefinition.ClassCode))
                throw new ArgumentException($"ClassCode of skill definition '{skillDefinition.Code}' is empty.");

            // ha nem létező SkillClass-ra hivatkozna a skill
            if (!await skillClassDefinitionRepo.TryGetByCode(skillDefinition.ClassCode, out var skillClass, cancellationToken))
                throw new ArgumentException($"Skill class not found with code '{skillDefinition.ClassCode}' referenced by skill definition '{skillDefinition.Code}'.");

            return skillClass;
        }

        private async Task<CodeOnlyAttribute[]> GetValidatedSkillCodeReferences(Background<CodeOnlyAttribute> backgroundDefinition, CodeOnlyAttribute[] skillCodeReferences, IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitionRepo, CancellationToken cancellationToken = default)
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
                if (!await skillDefinitionRepo.TryGetByCode(skillCodeRef.Code, out var skillDefinition, cancellationToken))
                {
                    int openBraceIndex = skillCodeRef.Code.IndexOf('(');
                    // ha van benne (
                    if (openBraceIndex > -1)
                    {
                        var code = skillCodeRef.Code.Substring(0, openBraceIndex).Trim();
                        // ha így már megtaláljuk a skill-t
                        if (await skillDefinitionRepo.TryGetByCode(code, out skillDefinition, cancellationToken))
                            continue;
                    }
                    throw new ArgumentException($"Skill not found with code '{skillCodeRef.Code}' referenced by background definition '{backgroundDefinition.Code}'.");
                }
            }

            return skillCodeReferences;
        }

        private async Task<CodeOnlyAttribute[]> GetValidatedTalentCodeReferences(Background<CodeOnlyAttribute> backgroundDefinition, CodeOnlyAttribute[] skillCodeReferences, IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitionRepo, CancellationToken cancellationToken = default)
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
                if (!await skillDefinitionRepo.TryGetByCode(skillCodeRef.Code, out var skillDefinition, cancellationToken))
                {
                    int openBraceIndex = skillCodeRef.Code.IndexOf('(');
                    // ha van benne (
                    if (openBraceIndex > -1)
                    {
                        var code = skillCodeRef.Code.Substring(0, openBraceIndex).Trim();
                        // ha így már megtaláljuk a skill-t
                        if (await skillDefinitionRepo.TryGetByCode(code, out skillDefinition, cancellationToken))
                            continue;
                    }
                    throw new ArgumentException($"Skill not found with code '{skillCodeRef.Code}' referenced by background definition '{backgroundDefinition.Code}'.");
                }
            }

            return skillCodeReferences;
        }

        private async Task ValidateCharacterClassAbilities(MagusTtkCharacterClassDefinitionDto characterClassDefinition, Dictionary<string, int> classAbilities, IReadOnlyRepository<AbilityDefinition> abilityRepo, CancellationToken cancellationToken = default)
        {
            if (characterClassDefinition == null)
                throw new ArgumentNullException(nameof(characterClassDefinition));
            if (classAbilities == null)
                throw new ArgumentNullException(nameof(classAbilities));
            if (abilityRepo == null)
                throw new ArgumentNullException(nameof(abilityRepo));

            int expectedAbilityCount = await abilityRepo.Count(null, cancellationToken);

            // ha nem a várt darabszámú tulajdonság alap érték lenne megadva
            if (classAbilities.Count != expectedAbilityCount)
                throw new ArgumentException($"Character class definition '{characterClassDefinition.Code}' has {classAbilities.Count} ability base value definitions, but should have {expectedAbilityCount}.");

            int abilityValueSum = 0;
            // leellenőrizzük, h minden tulajdonságra legyen megadva a kaszt specifikus alap érték
            foreach (var ability in await abilityRepo.All(null, cancellationToken))
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

        private async Task<List<TalentDefinition>> GetValidatedCharacterClassTalents(MagusTtkCharacterClassDefinitionDto characterClassDefinition, List<TalentDefinition> classTalents, IReadOnlyRepository<TalentDefinition> talentDefinitionRepo, CancellationToken cancellationToken = default)
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
                if (!await talentDefinitionRepo.TryGetByCode(classTalent.Code, out var talentDefinition, cancellationToken))
                    throw new ArgumentException($"Talent '{classTalent.Code}' in character class definition '{characterClassDefinition.Code}' is unknown.");

                talentDefinitionList.Add(talentDefinition);
            }

            return talentDefinitionList;
        }

        private async Task<MagusTtkCharacterSkills> GetValidatedCharacterClassSkills(MagusTtkCharacterClassDefinitionDto characterClassDefinition, SkillLevelDto[] classSkills, IReadOnlyRepository<MagusTtkSkillDefinition> skillDefinitionRepo, CancellationToken cancellationToken = default)
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
                if (!await skillDefinitionRepo.TryGetByCode(classSkill.Code, out var skillDefinition, cancellationToken))
                {
                    // lehet, hogy specializált képzettség, úgyhogy megpróbáljuk aszerint parse-olni
                    int firstIndex = classSkill.Code.IndexOf('(');
                    if (firstIndex < 0)
                        throw new ArgumentException($"Skill '{classSkill.Code}' in character class definition '{characterClassDefinition.Code}' is unknown.");

                    var key = classSkill.Code.Substring(0, firstIndex).Trim();
                    var spec = classSkill.Code.Substring(firstIndex + 1).Trim().TrimEnd(')');

                    if (!await skillDefinitionRepo.TryGetByCode(key, out skillDefinition, cancellationToken))
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

        public async Task InitializeWeaponDefinitionInMemoryRepositoryFromJson(MagusTtkContext dataContext, string jsonFilePath, CancellationToken cancellationToken = default)
        {
            string jsonString = await fileContentResolver.ReadFileAsTextAsync(jsonFilePath, cancellationToken);
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException($"Json file '{jsonFilePath}' not found.");

            var entitiesFromJson = JsonDeserializer.DeserializeFromJsonString<MagusTtkWeaponDefinition[]>(jsonString);

            // JSON séma hiba
            if ((entitiesFromJson == null) || (entitiesFromJson.Length == 0))
                throw new ArgumentException($"Could not parse entities of type '{typeof(MagusTtkWeaponDefinition)}' from Json file '{jsonFilePath}'.");

            var editableRepo = dataContext.WeaponDefinitions.AsEditableRepository();
            foreach (var entity in entitiesFromJson)
            {
                // hiányzó Code
                if (string.IsNullOrWhiteSpace(entity.Code))
                    throw new ArgumentException($"Weapon code in file '{jsonFilePath}' is empty.");

                // ha már van ilyen kóddal már fegyver
                if (await dataContext.WeaponDefinitions.ExistsByCode(entity.Code, cancellationToken))
                    throw new ArgumentException($"Weapon code '{entity.Code}' in file '{jsonFilePath}' is a duplicate.");

                // ha nincs megadva egy fegyver kategória sem
                if ((entity.CategoryCodes == null) || (entity.CategoryCodes.Length == 0))
                    throw new ArgumentException($"CategoryCodes are missing for weapon code '{entity.Code}' in file '{jsonFilePath}'.");

                foreach (var weaponCategoryCode in entity.CategoryCodes)
                {
                    // ha nem valid fegyver kategória van megadva
                    if (!await dataContext.WeaponCategoryDefinitions.TryGetByCode(weaponCategoryCode, out var weaponCategory, cancellationToken))
                        throw new ArgumentException($"Weapon category code '{weaponCategoryCode}' in weapon definition '{entity.Code}' is unknown.");
                }

                // végül megpróbáljuk hozzáadni (elszáll, ha már lenne ilyen kóddal)
                await editableRepo.Add(entity, cancellationToken);
            }
        }

        public async Task InitializeData(MagusTtkContext dataContext, CancellationToken cancellationToken)
        {
            if (!dataContext.IsDataInitialized)
            {
                await InitializeInMemoryRepositoryFromJson<AbilityDefinition>(dataContext, dataContext.AbilityDefinitions, "abilities.json", cancellationToken);
                await InitializeInMemoryRepositoryFromJson<SkillCategory>(dataContext, dataContext.SkillCategoryDefinitions, "skillCategories.json", cancellationToken);
                await InitializeInMemoryRepositoryFromJson<MagusTtkSkillClassDefinition>(dataContext, dataContext.SkillClassDefinitions, "skillClasses.json", cancellationToken);
                await InitializeInMemoryRepositoryFromJson<WeaponCategory>(dataContext, dataContext.WeaponCategoryDefinitions, "weaponCategories.json", cancellationToken);
                await InitializeSkillDefinitionInMemoryRepositoryFromJson(dataContext, "skills.json", cancellationToken);
                await InitializeTraitDefinitionInMemoryRepositoryFromJson(dataContext, "traits.json", cancellationToken);
                await InitializeTalentDefinitionInMemoryRepositoryFromJson(dataContext, "talents.json", cancellationToken);
                await InitializeBackgroundDefinitionInMemoryRepositoryFromJson(dataContext, "origins.json", cancellationToken);
                await InitializeCharacterClassDefinitionInMemoryRepositoryFromJson(dataContext, "characterClasses.json", cancellationToken);
                await InitializeWeaponDefinitionInMemoryRepositoryFromJson(dataContext, "weapons.json", cancellationToken);

                dataContext.IsDataInitialized = true;
            }
        }
    }
}
