using System;
using System.Collections.Generic;
using System.Linq;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Egy korkategória
    /// </summary>
    public class Aging
    {
        private readonly AgingCategory[] Categories;

        public Aging(IEnumerable<AgingCategory> agingCategories)
        {
            this.Categories = agingCategories.ToArray();
        }

        /// <summary>
        /// Kiszámolja, hogy egy adott korú karakternek hányszor kell öregedési próbát dobnia ebben a korkategóriában.
        /// </summary>
        /// <param name="age">A karakter kora, amire a módosítót ki kell számolni.</param>
        /// <returns></returns>
        public int GetAgingRollsCount(decimal age)
        {
            if ((this.Categories == null) || (this.Categories.Length == 0))
                return 0;

            int agingRollsCount = 0;
            // minden korkategóriára
            foreach (var categ in this.Categories)
            {
                if (categ.FromAge > age)
                    break;

                // kiszámoljuk, hogy abban a korkategóriában hányszor kell öregedési próbát dobnia
                agingRollsCount += categ.GetAgingRollsCount(age);
            }

            return agingRollsCount;
        }

        /// <summary>
        /// Kiszámolja, hogy egy adott korú karakter mennyi Sp-t kap.
        /// </summary>
        /// <param name="age">A karakter kora, amire a módosítót ki kell számolni.</param>
        /// <returns></returns>
        public int GetFreeSp(decimal age)
        {
            if ((this.Categories == null) || (this.Categories.Length == 0))
                return 0;

            int freeSp = 0;
            // minden korkategóriára
            foreach (var categ in this.Categories)
            {
                if (categ.FromAge > age)
                    break;

                // kiszámoljuk, hogy abban a korkategóriában mekkora tulajdonság módosítót kap
                freeSp += categ.GetFreeSp(age);
            }

            return freeSp;
        }
    }
}
