using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace MAGUS.TTK.Domain.Test.Unit
{
    [TestClass]
    public class MagusTtkContextServiceInitializerUnitTests
    {
        [TestMethod]
        public void Test_MagusTtkContextServiceInitializer_Initialize()
        {
            var initializer = new MagusTtkContextServiceInitializer();
            var serviceCollection = new ServiceCollection();
            initializer.Initialize(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var ctx = serviceProvider.GetRequiredService<MagusTtkContext>();

            Assert.AreNotEqual(0, ctx.AbilityDefinitions.Count());
            Assert.AreNotEqual(0, ctx.OriginDefinitions.Count());
            Assert.AreNotEqual(0, ctx.SkillCategoryDefinitions.Count());
            Assert.AreNotEqual(0, ctx.SkillClassDefinitions.Count());
            Assert.AreNotEqual(0, ctx.SkillDefinitions.Count());
            Assert.AreNotEqual(0, ctx.TraitDefinitions.Count());
            Assert.AreNotEqual(0, ctx.WeaponCategoryDefinitions.Count());
            Assert.AreNotEqual(0, ctx.CharacterClassDefinitions.Count());
            Assert.AreNotEqual(0, ctx.WeaponDefinitions.Count());

            foreach (var charClassDef in ctx.CharacterClassDefinitions.List())
            {
                System.Diagnostics.Debug.WriteLine($"Skills of charcter class '{charClassDef.Name ?? charClassDef.Code}' by category:");

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
        }
    }
}
