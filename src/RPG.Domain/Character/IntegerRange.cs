using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain.Character
{
    public class IntegerRange
    {
        public IntegerRange(int minValue, int maxValue)
        {
        }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
