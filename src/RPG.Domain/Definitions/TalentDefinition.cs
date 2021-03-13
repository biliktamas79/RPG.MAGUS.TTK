using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of talent
    /// </summary>
    public class TalentDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Talent unique code
        /// </summary>
        public string Code { get; set; }
    }
}
