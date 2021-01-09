using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Character
{
    public class IntegerAttribute : AttributeBase
    {
        public IntegerAttribute(string code) : base(code)
        {
        }

        public int Value { get; set; }
    }
}
