using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonIgnoreAttribute]
        public TraitDefinition Definition { get; private set; }
        /// <summary>
        /// Jellemvonás aktuális számszerű értéke
        /// </summary>
        public int Value;
    }
}
