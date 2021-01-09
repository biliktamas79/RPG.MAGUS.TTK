using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Character
{
    public class CharacterBuilder
    {
        private readonly RpgRuleSet ruleSet;

        public CharacterBuilder(RpgRuleSet ruleSet)
        {
            this.ruleSet = ruleSet ?? throw new ArgumentNullException(nameof(ruleSet));
        }

        //TODO public Build
    }
}
