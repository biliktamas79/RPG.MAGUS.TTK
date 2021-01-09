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
        Race = 1,
        Class = 2,
        CharacterCreation = 3,
        LevelUp = 4,
        BoughtFromSP = 5,
    }
}
