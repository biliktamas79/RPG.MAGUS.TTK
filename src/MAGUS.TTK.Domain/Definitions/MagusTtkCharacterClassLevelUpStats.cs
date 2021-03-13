namespace MAGUS.TTK.Domain.Definitions
{
    public class MagusTtkCharacterClassLevelUpStats
    {
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
        /// Sors pont
        /// </summary>
        public int Sp { get; set; }

        public int CalculateSpCostSum()
        {
            int sum = (this.FreeHm + this.TE + this.VE + this.CE) * 2
                + this.KE * 3
                + this.Fp * 4
                + this.Kp * 1
                + (this.Pp + this.Mp) * 2
                + this.Sp;
            return sum;
        }
    }
}
