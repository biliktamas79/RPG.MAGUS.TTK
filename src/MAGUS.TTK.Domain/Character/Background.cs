using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// Származás/Neveltetés jellegű háttér
    /// </summary>
    public class Background<T> : IHasUniqueCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public T[] Advantages { get; set; }
        public T[] Disadvantages { get; set; }

        public override string ToString()
        {
            return this.Name ?? this.Code;
        }
    }
}
