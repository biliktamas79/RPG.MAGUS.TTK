using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// Enumeration describing the possible types of attribute value components.
    /// </summary>
    public enum AbilityValueComponentTypeEnum : int
    {
        /// <summary>
        /// Fajból adódó
        /// </summary>
        Race = 1,
        /// <summary>
        /// Kasztból adódó
        /// </summary>
        Class = 2,
        /// <summary>
        /// Karakter alkotásból származó
        /// </summary>
        CharacterCreation = 3,
        /// <summary>
        /// Szintlépésből adódó
        /// </summary>
        LevelUp = 4,
        /// <summary>
        /// SP-ből vásárolt
        /// </summary>
        BoughtFromSP = 5,
    }
}
