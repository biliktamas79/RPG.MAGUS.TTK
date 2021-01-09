namespace MAGUS.TTK.Domain.Character
{
    public class MagusTtkCharacterStatsDto
    {
        /// <summary>
        /// Kezdeményező érték
        /// </summary>
        public StatsValue KE { get; set; }
        /// <summary>
        /// Támadó érték
        /// </summary>
        public StatsValue TE { get; set; }
        /// <summary>
        /// Védekező érték
        /// </summary>
        public StatsValue VE { get; set; }
        /// <summary>
        /// Célzó érték
        /// </summary>
        public StatsValue CE { get; set; }
        /// <summary>
        /// Életerő pont
        /// </summary>
        public StatsValue Ep { get; set; }
        /// <summary>
        /// Fájdalomtűrés pont
        /// </summary>
        public StatsValue Fp { get; set; }
        /// <summary>
        /// Pszi pont
        /// </summary>
        public StatsValue Pp { get; set; }
        /// <summary>
        /// Mana pont
        /// </summary>
        public StatsValue Mp { get; set; }
        /// <summary>
        /// Képzettség pont
        /// </summary>
        public StatsValue Kp { get; set; }
        /// <summary>
        /// Sors pont
        /// </summary>
        public StatsValue Sp { get; set; }
        /// <summary>
        /// Legenda pont
        /// </summary>
        public StatsValue Lp { get; set; }
    }
}
