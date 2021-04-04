using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Data;
using MAGUS.TTK.Domain.Definitions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace MAGUS.TTK.Domain.Test.Unit
{
    [TestClass]
    public class MagusTtkContextServiceInitializerUnitTests
    {
        [TestMethod]
        public async Task Test_MagusTtkContextServiceInitializer_Initialize()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IFileContentResolver>(serviceProvider => new LocalFileContentResolver("Definitions/"));

            var initializer = new MagusTtkContextServiceInitializer();
            initializer.RegisterServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var ctx = serviceProvider.GetRequiredService<MagusTtkContext>();
            var dataInitializer = serviceProvider.GetRequiredService<IDataInitializer<MagusTtkContext>>();

            await dataInitializer.InitializeData(ctx, CancellationToken.None);

            Assert.AreNotEqual(0, ctx.AbilityDefinitions.Count());
            Assert.AreNotEqual(0, ctx.OriginDefinitions.Count());
            Assert.AreNotEqual(0, ctx.SkillCategoryDefinitions.Count());
            Assert.AreNotEqual(0, ctx.SkillClassDefinitions.Count());
            Assert.AreNotEqual(0, ctx.SkillDefinitions.Count());
            Assert.AreNotEqual(0, ctx.TraitDefinitions.Count());
            Assert.AreNotEqual(0, ctx.WeaponCategoryDefinitions.Count());
            Assert.AreNotEqual(0, ctx.CharacterClassDefinitions.Count());
            Assert.AreNotEqual(0, ctx.WeaponDefinitions.Count());
            Assert.AreNotEqual(0, ctx.RaceDefinitions.Count());

            foreach (var charClassDef in await ctx.CharacterClassDefinitions.All(null, CancellationToken.None))
            {
                System.Diagnostics.Debug.WriteLine($"Skills of character class '{charClassDef.Name ?? charClassDef.Code}' by category:");

                foreach (var group in charClassDef.Skills.GetSkillsByCategory())
                {
                    System.Diagnostics.Debug.WriteLine($"  Skill category '{group.Key.Name ?? group.Key.Code}'");

                    foreach (var skill in group)
                    {
                        System.Diagnostics.Debug.WriteLine($"    - {skill}");
                    }
                }

                System.Diagnostics.Debug.WriteLine("");
            }

            System.Diagnostics.Debug.WriteLine("");

            foreach (var group in (await ctx.SkillDefinitions.All(null, CancellationToken.None)).GroupSkillDefinitionsByCategory())
            {
                System.Diagnostics.Debug.WriteLine($"Skills of category '{group.Key.Name ?? group.Key.Code}':");

                string prevGroupId = null;

                foreach (var skill in group)
                {
                    if ((prevGroupId != null) && (prevGroupId != skill.Group))
                        System.Diagnostics.Debug.WriteLine("  ");

                    System.Diagnostics.Debug.WriteLine($"  - {skill}");

                    prevGroupId = skill.Group;
                }

                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}
