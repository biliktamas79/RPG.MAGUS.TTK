using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// Enumeration describing the possible types of stats value components.
    /// </summary>
    public enum StatsValueComponentTypeEnum : int
    {
        Race = 1,
        Class = 2,
        LevelUp = 4,
        BoughtFromSP = 8,
        Ability = 16
    }
}
