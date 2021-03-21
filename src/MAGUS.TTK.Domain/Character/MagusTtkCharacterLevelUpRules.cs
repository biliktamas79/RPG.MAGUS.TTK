using System.Text;

namespace MAGUS.TTK.Domain.Character
{
    public class MagusTtkCharacterLevelUpRules
    {
        /// <summary>
        /// Az új szint, amire fellépve érvényesek ezek a szintlépés miatti növekmények.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Hány ingyenes tulajdonság növekményt kap a szint meglépésekor?
        /// </summary>
        public int FreeAbilityCount { get; set; }
        /// <summary>
        /// Hány ingyenes adottság növekményt kap a szint meglépésekor?
        /// </summary>
        public int FreeTalentCount { get; set; }
        /// <summary>
        /// Mennyi Legenda pont növekményt kap a szint meglépésekor?
        /// </summary>
        public int LpIncrease { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Level).Append(". (");
            if (this.LpIncrease > 0)
            {
                sb.Append(" +").Append(this.LpIncrease).Append(" Lp");
            }
            if (this.FreeAbilityCount > 0)
            {
                sb.Append(", +").Append(this.FreeAbilityCount).Append(" tulajdonság pont növekmény");
            }
            if (this.FreeTalentCount > 0)
            {
                sb.Append(", +").Append(this.FreeTalentCount).Append(" adottság fokozat");
            }
            sb.Append(")");

            return sb.ToString();
        }
    }
}
