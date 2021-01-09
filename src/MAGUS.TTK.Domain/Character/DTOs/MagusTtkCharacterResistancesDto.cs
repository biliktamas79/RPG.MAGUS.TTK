namespace MAGUS.TTK.Domain.Character
{
    public class MagusTtkCharacterResistancesDto
    {
        /// <summary>
        /// Összetett ellenállás
        /// </summary>
        public IntValueWithDiceModifierDto Osszetett { get; set; }
        /// <summary>
        /// Fizikai ellenállás
        /// </summary>
        public IntValueWithDiceModifierDto Fizikai { get; set; }
        /// <summary>
        /// Szellemi ellenállás
        /// </summary>
        public IntValueWithDiceModifierDto Szellemi { get; set; }
        /// <summary>
        /// Asztrális ellenállás
        /// </summary>
        public IntValueWithDiceModifierDto Asztralis { get; set; }
        /// <summary>
        /// Mentális ellenállás
        /// </summary>
        public IntValueWithDiceModifierDto Mentalis { get; set; }
        /// <summary>
        /// Elkerülő ellenállás
        /// </summary>
        public IntValueWithDiceModifierDto Elkerulo { get; set; }
    }
}
