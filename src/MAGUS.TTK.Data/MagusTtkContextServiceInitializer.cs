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

namespace MAGUS.TTK.Data
{
    public class MagusTtkContextServiceInitializer
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MagusTtkContext>()
                .AddSingleton<IDataInitializer<MagusTtkContext>, MagusTtkDataContextInitializer>()
                // repositories
                .AddSingleton<IReadOnlyRepository<AbilityDefinition>, InMemoryRepository<AbilityDefinition>>()
                .AddSingleton<IReadOnlyRepository<SkillCategory>, InMemoryRepository<SkillCategory>>()
                .AddSingleton<IReadOnlyRepository<MagusTtkSkillClassDefinition>, InMemoryRepository<MagusTtkSkillClassDefinition>>()
                .AddSingleton<IReadOnlyRepository<MagusTtkSkillDefinition>, InMemoryRepository<MagusTtkSkillDefinition>>()
                .AddSingleton<IReadOnlyRepository<WeaponCategory>, InMemoryRepository<WeaponCategory>>()
                .AddSingleton<IReadOnlyRepository<TraitDefinition>, InMemoryRepository<TraitDefinition>>()
                .AddSingleton<IReadOnlyRepository<TalentDefinition>, InMemoryRepository<TalentDefinition>>()
                .AddSingleton<IReadOnlyRepository<Background<CodeOnlyAttribute>>, InMemoryRepository<Background<CodeOnlyAttribute>>>()
                .AddSingleton<IReadOnlyRepository<MagusTtkCharacterClassDefinition>, InMemoryRepository<MagusTtkCharacterClassDefinition>>()
                .AddSingleton<IReadOnlyRepository<MagusTtkWeaponDefinition>, InMemoryRepository<MagusTtkWeaponDefinition>>()
                ;

            //InitializeInMemoryRepositoryInstances(serviceCollection);
        }
    }
}
