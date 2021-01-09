using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of a character trait
    /// </summary>
    public class TraitDefinition : IHasUniqueCode
    {
        public string Code { get; private set; }
        public int MinValue;
        public int MaxValue;
    }
}
