using System;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter jellemvonás, mint pl. becsületes, hűséges, megbízható, ...
    /// </summary>
    public class TraitValue
    {
        /// <summary>
        /// Jellemvonás definíció
        /// </summary>
        public TraitDefinition Definition { get; private set; }
        /// <summary>
        /// Jellemvonás aktuális számszerű értéke
        /// </summary>
        public int Value;
    }
}
