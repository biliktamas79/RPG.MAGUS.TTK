using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Character
{
    [Flags]
    public enum GenderEnum : byte
    {
        None = 0,
        Male = 1, 
        Female = 2, 
        Both = 3
    }
}
