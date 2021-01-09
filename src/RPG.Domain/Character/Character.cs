using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Character
{
    public class Character
    {
        private readonly Dictionary<string, AttributeGroup<AttributeBase>> attributeGroupsByCode;

        public Character(Dictionary<string, AttributeGroup<AttributeBase>> attributeGroupsByCode)
        {
            this.attributeGroupsByCode = attributeGroupsByCode ?? throw new ArgumentNullException(nameof(attributeGroupsByCode));
        }
    }
}
