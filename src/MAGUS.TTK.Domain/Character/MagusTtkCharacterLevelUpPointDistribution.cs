using MAGUS.TTK.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    public class MagusTtkCharacterLevelUpPointDistribution
    {
        /// <summary>
        /// TTK szabályrendszer általános szintlépés szabályai
        /// </summary>
        public MagusTtkCharacterLevelUpRules GeneralLevelUpRules;
        /// <summary>
        /// A kaszt-specifikus szintlépés növekmények
        /// </summary>
        public MagusTtkCharacterClassLevelUpStats CharacterClassLevelUpStats;
        /// <summary>
        /// The new level the character reached.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Szabadon elosztható HM
        /// </summary>
        public int FreeHm { get; set; }
        /// <summary>
        /// Támadó érték
        /// </summary>
        public int TE { get; set; }
        /// <summary>
        /// Védekező érték
        /// </summary>
        public int VE { get; set; }
        /// <summary>
        /// Célzó érték
        /// </summary>
        public int CE { get; set; }
        /// <summary>
        /// Kezdeményező érték
        /// </summary>
        public int KE { get; set; }
        /// <summary>
        /// Fájdalomtűrés pont
        /// </summary>
        public int Fp { get; set; }
        /// <summary>
        /// Képzettség pont
        /// </summary>
        public int Kp { get; set; }
        /// <summary>
        /// Pszi pont
        /// </summary>
        public int Pp { get; set; }
        /// <summary>
        /// Mana pont
        /// </summary>
        public int Mp { get; set; }
        /// <summary>
        /// Még nem elköltött sors pont
        /// </summary>
        public int FreeSp { get; set; }

        public AbilityIncrease FreeAbilityIncrease { get; set; }
        public TalentIncrease FreeTalentIncrease { get; set; }

        public int CalculateSpCostSum()
        {
            int sum = (this.FreeHm + this.TE + this.VE + this.CE) * 2
                + this.KE * 3
                + this.Fp * 4
                + this.Kp * 1
                + (this.Pp + this.Mp) * 2
                + this.FreeSp;
            return sum;
        }
    }
}
