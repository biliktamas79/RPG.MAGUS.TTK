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
        [JsonIgnore]
        public TraitDefinition Definition { get; private set; }
        /// <summary>
        /// Jellemvonás aktuális számszerű értéke
        /// </summary>
        public int Value;

        public override string ToString()
        {
            return $"{this.Definition.Code}: {this.Value}";
        }
    }
}
