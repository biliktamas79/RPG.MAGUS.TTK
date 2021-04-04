using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAGUS.TTK.Domain.Definitions;

namespace MAGUS.TTK.Domain.Test.Unit
{
    [TestClass]
    public class AgingUnitTests
    {
        // Az 'ember' faj korkategóriái
        private static readonly AgingCategory[] humanAging = new AgingCategory[]
            {
                    new AgingCategory() { Name = "gyermek", FromAge = 0, ToAge = 12, SpPerYear = 0 },
                    new AgingCategory() { Name = "serdülő", FromAge = 13, ToAge = 16, SpPerYear = 0 },
                    new AgingCategory() { Name = "ifjú", FromAge = 17, ToAge = 30, SpPerYear = 8, SpPerTimeUnit = 8, SpTimeUnitYears = 1 },
                    new AgingCategory() { Name = "középkorú", FromAge = 31, ToAge = 42, SpPerYear = 4, SpPerTimeUnit = 4, SpTimeUnitYears = 1 },
                    new AgingCategory() { Name = "meglett", FromAge = 43, ToAge = 55, SpPerYear = 2, SpPerTimeUnit = 2, SpTimeUnitYears = 1, AgingFactor = 1 },
                    new AgingCategory() { Name = "idős", FromAge = 56, ToAge = 75, SpPerYear = 1, SpPerTimeUnit = 1, SpTimeUnitYears = 1, AgingFactor = 1m / 2m },
                    new AgingCategory() { Name = "agg", FromAge = 76, SpPerYear = 1m / 2m, SpPerTimeUnit = 1, SpTimeUnitYears = 2, AgingFactor = 1m / 3m },
            };

        // A 'törpe' faj korkategóriái
        private static readonly AgingCategory[] dwarfAging = new AgingCategory[]
            {
                    new AgingCategory() { Name = "gyermek", FromAge = 0, ToAge = 24, SpPerYear = 0 },
                    new AgingCategory() { Name = "serdülő", FromAge = 25, ToAge = 40, SpPerYear = 0 },
                    new AgingCategory() { Name = "ifjú", FromAge = 41, ToAge = 376, SpPerYear = 0.33334m, SpPerTimeUnit = 1, SpTimeUnitYears = 3 },
                    new AgingCategory() { Name = "középkorú", FromAge = 377, ToAge = 616, SpPerYear = 1m / 5m, SpPerTimeUnit = 1, SpTimeUnitYears = 5 },
                    new AgingCategory() { Name = "meglett", FromAge = 617, ToAge = 694, SpPerYear = 0.33334m, SpPerTimeUnit = 1, SpTimeUnitYears = 3, AgingFactor = 8 },
                    new AgingCategory() { Name = "idős", FromAge = 695, ToAge = 754, SpPerYear = 0.33334m, SpPerTimeUnit = 1, SpTimeUnitYears = 3, AgingFactor = 3 },
                    new AgingCategory() { Name = "agg", FromAge = 755, SpPerYear = 1m / 15m, SpPerTimeUnit = 1, SpTimeUnitYears = 15, AgingFactor = 2 },
            };

        [TestMethod]
        public void Aging_GetFreeSp_returns_the_right_value_for_humans()
        {
            Assert.AreEqual(0, humanAging.GetFreeSp(16));
            Assert.AreEqual(8, humanAging.GetFreeSp(17));
            Assert.AreEqual(16, humanAging.GetFreeSp(18));
            
            Assert.AreEqual(24, humanAging.GetFreeSp(19));
            Assert.AreEqual(32, humanAging.GetFreeSp(20));
            Assert.AreEqual(40, humanAging.GetFreeSp(21));
            
            Assert.AreEqual(48, humanAging.GetFreeSp(22));
            Assert.AreEqual(56, humanAging.GetFreeSp(23));
            Assert.AreEqual(64, humanAging.GetFreeSp(24));

            Assert.AreEqual(72, humanAging.GetFreeSp(25));

            Assert.AreEqual(96, humanAging.GetFreeSp(28));
            
            Assert.AreEqual(112, humanAging.GetFreeSp(30));
            Assert.AreEqual(116, humanAging.GetFreeSp(31));
            Assert.AreEqual(120, humanAging.GetFreeSp(32));
            Assert.AreEqual(136, humanAging.GetFreeSp(36));
            
            Assert.AreEqual(160, humanAging.GetFreeSp(42));
            Assert.AreEqual(162, humanAging.GetFreeSp(43));
            Assert.AreEqual(164, humanAging.GetFreeSp(44));

            Assert.AreEqual(174, humanAging.GetFreeSp(49)); // a 0.84 PDF szbálykönyvben 173 van, de emberek esetében a 174 a helyes.

            Assert.AreEqual(186, humanAging.GetFreeSp(55));

            Assert.AreEqual(196, humanAging.GetFreeSp(65));
            
            Assert.AreEqual(206, humanAging.GetFreeSp(75));
        }

        [TestMethod]
        public void Aging_GetFreeSp_returns_the_right_value_for_dwarfs()
        {
            Assert.AreEqual(0, dwarfAging.GetFreeSp(16));
            Assert.AreEqual(0, dwarfAging.GetFreeSp(40));
            Assert.AreEqual(0, dwarfAging.GetFreeSp(41));
            Assert.AreEqual(0, dwarfAging.GetFreeSp(42));
            Assert.AreEqual(1, dwarfAging.GetFreeSp(43));
            Assert.AreEqual(1, dwarfAging.GetFreeSp(44));
            Assert.AreEqual(1, dwarfAging.GetFreeSp(45));
            Assert.AreEqual(2, dwarfAging.GetFreeSp(46));

            Assert.AreEqual(24, dwarfAging.GetFreeSp(112));

            Assert.AreEqual(48, dwarfAging.GetFreeSp(184));

            Assert.AreEqual(72, dwarfAging.GetFreeSp(256));

            Assert.AreEqual(96, dwarfAging.GetFreeSp(328));

            Assert.AreEqual(112, dwarfAging.GetFreeSp(376));
            
            Assert.AreEqual(136, dwarfAging.GetFreeSp(496));

            Assert.AreEqual(160, dwarfAging.GetFreeSp(616));

            Assert.AreEqual(173, dwarfAging.GetFreeSp(655));

            Assert.AreEqual(186, dwarfAging.GetFreeSp(694));

            Assert.AreEqual(196, dwarfAging.GetFreeSp(724));

            Assert.AreEqual(206, dwarfAging.GetFreeSp(754));
        }

        [TestMethod]
        public void Aging_GetAgingRollsCount_returns_the_right_value_for_humans()
        {
            Assert.AreEqual(0, humanAging.GetAgingRollsCount(16));
            Assert.AreEqual(0, humanAging.GetAgingRollsCount(17));
            Assert.AreEqual(0, humanAging.GetAgingRollsCount(18));

            Assert.AreEqual(0, humanAging.GetAgingRollsCount(42));
            Assert.AreEqual(1, humanAging.GetAgingRollsCount(43));
            Assert.AreEqual(2, humanAging.GetAgingRollsCount(44));
            Assert.AreEqual(3, humanAging.GetAgingRollsCount(45));

            Assert.AreEqual(7, humanAging.GetAgingRollsCount(49));

            Assert.AreEqual(13, humanAging.GetAgingRollsCount(55));
            Assert.AreEqual(13, humanAging.GetAgingRollsCount(55.5m));

            Assert.AreEqual(15, humanAging.GetAgingRollsCount(56));
            Assert.AreEqual(16, humanAging.GetAgingRollsCount(56.5m));
            Assert.AreEqual(17, humanAging.GetAgingRollsCount(57));
            Assert.AreEqual(18, humanAging.GetAgingRollsCount(57.5m));
            Assert.AreEqual(19, humanAging.GetAgingRollsCount(58));
            Assert.AreEqual(21, humanAging.GetAgingRollsCount(59));
            Assert.AreEqual(23, humanAging.GetAgingRollsCount(60));

            Assert.AreEqual(33, humanAging.GetAgingRollsCount(65));

            Assert.AreEqual(51, humanAging.GetAgingRollsCount(74));
            Assert.AreEqual(53, humanAging.GetAgingRollsCount(75));
            Assert.AreEqual(56, humanAging.GetAgingRollsCount(76));
            Assert.AreEqual(57, humanAging.GetAgingRollsCount(76.334m));
            Assert.AreEqual(57, humanAging.GetAgingRollsCount(76.5m));
            Assert.AreEqual(58, humanAging.GetAgingRollsCount(76.667m));
            Assert.AreEqual(59, humanAging.GetAgingRollsCount(77m));
        }

        [TestMethod]
        public void Aging_GetAgingRollsCount_returns_the_right_value_for_dwarfs()
        {
            Assert.AreEqual(0, dwarfAging.GetAgingRollsCount(16));
            Assert.AreEqual(0, dwarfAging.GetAgingRollsCount(17));
            Assert.AreEqual(0, dwarfAging.GetAgingRollsCount(18));

            Assert.AreEqual(0, dwarfAging.GetAgingRollsCount(617));
            Assert.AreEqual(0, dwarfAging.GetAgingRollsCount(623));
            Assert.AreEqual(1, dwarfAging.GetAgingRollsCount(624));
            Assert.AreEqual(1, dwarfAging.GetAgingRollsCount(625));

            Assert.AreEqual(1, dwarfAging.GetAgingRollsCount(631));
            Assert.AreEqual(2, dwarfAging.GetAgingRollsCount(632));
            Assert.AreEqual(2, dwarfAging.GetAgingRollsCount(633));

            //Assert.AreEqual(2, dwarfAging.GetAgingRollsCount(639));
            //Assert.AreEqual(3, dwarfAging.GetAgingRollsCount(640));
            //Assert.AreEqual(3, dwarfAging.GetAgingRollsCount(641));

            Assert.AreEqual(4, dwarfAging.GetAgingRollsCount(655));
            Assert.AreEqual(5, dwarfAging.GetAgingRollsCount(656)); // a 0.84 PDF-ben 655 szerepel, de a 8-evénként miatt 656-nál lesz 5
            Assert.AreEqual(5, dwarfAging.GetAgingRollsCount(657));

            Assert.AreEqual(5, dwarfAging.GetAgingRollsCount(663));
            Assert.AreEqual(6, dwarfAging.GetAgingRollsCount(664));
            Assert.AreEqual(6, dwarfAging.GetAgingRollsCount(665));

            //Assert.AreEqual(6, dwarfAging.GetAgingRollsCount(671));
            //Assert.AreEqual(7, dwarfAging.GetAgingRollsCount(672));
            //Assert.AreEqual(7, dwarfAging.GetAgingRollsCount(673));

            //Assert.AreEqual(7, dwarfAging.GetAgingRollsCount(679));
            //Assert.AreEqual(8, dwarfAging.GetAgingRollsCount(680));
            //Assert.AreEqual(8, dwarfAging.GetAgingRollsCount(681));

            Assert.AreEqual(9, dwarfAging.GetAgingRollsCount(694));
            Assert.AreEqual(10, dwarfAging.GetAgingRollsCount(695));
            Assert.AreEqual(10, dwarfAging.GetAgingRollsCount(696));
            Assert.AreEqual(10, dwarfAging.GetAgingRollsCount(697));
            Assert.AreEqual(11, dwarfAging.GetAgingRollsCount(698));

            Assert.AreEqual(18, dwarfAging.GetAgingRollsCount(721));
            Assert.AreEqual(19, dwarfAging.GetAgingRollsCount(722));
            Assert.AreEqual(19, dwarfAging.GetAgingRollsCount(723));
            Assert.AreEqual(19, dwarfAging.GetAgingRollsCount(724));
            Assert.AreEqual(20, dwarfAging.GetAgingRollsCount(725));
            Assert.AreEqual(20, dwarfAging.GetAgingRollsCount(726));
            Assert.AreEqual(20, dwarfAging.GetAgingRollsCount(727));
            Assert.AreEqual(21, dwarfAging.GetAgingRollsCount(728));
            Assert.AreEqual(21, dwarfAging.GetAgingRollsCount(729));

            Assert.AreEqual(28, dwarfAging.GetAgingRollsCount(751));
            Assert.AreEqual(29, dwarfAging.GetAgingRollsCount(752));
            Assert.AreEqual(29, dwarfAging.GetAgingRollsCount(753));
            Assert.AreEqual(29, dwarfAging.GetAgingRollsCount(754));
            Assert.AreEqual(30, dwarfAging.GetAgingRollsCount(755));
            Assert.AreEqual(30, dwarfAging.GetAgingRollsCount(756));
            Assert.AreEqual(31, dwarfAging.GetAgingRollsCount(757));
            Assert.AreEqual(31, dwarfAging.GetAgingRollsCount(758));
            Assert.AreEqual(32, dwarfAging.GetAgingRollsCount(759));
            Assert.AreEqual(32, dwarfAging.GetAgingRollsCount(760));
        }
    }
}
