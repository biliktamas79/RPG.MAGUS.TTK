using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Character
{
    public class CodeOnlyAttribute : AttributeBase
    {
        public CodeOnlyAttribute(string code) : base(code)
        {
        }
    }
}
