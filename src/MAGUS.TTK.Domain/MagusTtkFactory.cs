using MAGUS.TTK.Domain.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAGUS.TTK.Domain
{
    public class MagusTtkFactory
    {
        public MagusTtkDefinitions Definitions { get; set; }

        public Character.MagusTtkCharacter NewCharacter()
        {
            var c = new Character.MagusTtkCharacter();
            InitCharacterAbilities(c);

            return c;
        }

        private void InitCharacterAbilities(Character.MagusTtkCharacter c)
        {
            //foreach (var ability in this.Definitions.CharacterAbilities)
            //{
            //    c.Abilities.Add(ability.Key, new Character.AbilityValue() { Definition = ability.Value });
            //}

            //this.Definitions.CharacterAbilities.Select(kvp => )
        }
    }
}
