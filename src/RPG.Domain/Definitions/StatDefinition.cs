using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Definitions
{
    /// <summary>
    /// Definition of a character statistics
    /// </summary>
    public class StatDefinition : IHasUniqueCode
    {
        /// <summary>
        /// Statistic unique code, like KÉ, TÉ, VÉ, Mp, Pp, ...
        /// </summary>
        public string Code { get; private set; }
    }
}
