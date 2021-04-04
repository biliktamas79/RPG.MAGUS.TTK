using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Egy korkategória
    /// </summary>
    public class AgingCategory
    {
        /// <summary>
        /// A korkategória neve
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A korkategória alsó korlátja
        /// </summary>
        public int FromAge { get; set; }
        /// <summary>
        /// A korkategória felső korlátja
        /// </summary>
        [JsonIgnore]
        public int? ToAge { get; set; }
        /// <summary>
        /// Mennyi idő elteltével kell a kor kategória miatt öregedés próbát dobni.
        /// </summary>
        public decimal? AgingFactor { get; set; }
        /// <summary>
        /// Hány Sp-t kap évente.
        /// </summary>
        public decimal SpPerYear { get; set; }
        /// <summary>
        /// Hány Sp-t kap időegységenként.
        /// </summary>
        public int SpPerTimeUnit { get; set; }
        /// <summary>
        /// Az korból számítandó Sp-nél hány év számít egy időegységnek.
        /// </summary>
        public int SpTimeUnitYears { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.FromAge} - {this.ToAge})";
        }

        public string GetAgeRangeString()
        {
            return (this.ToAge == null)
                ? $"{this.FromAge}-"
                : $"{this.FromAge}-{this.ToAge.Value}";
        }

        /// <summary>
        /// Kiszámolja, hogy egy adott korú karakternek hányszor kell öregedési próbát dobnia ebben a korkategóriában.
        /// </summary>
        /// <param name="age">A karakter kora, amire a módosítót ki kell számolni.</param>
        /// <returns></returns>
        public decimal GetAgingRollsCount(decimal age)
        {
            // ha ebben a korkategóriában nincs tulajdonság pont módosító
            if ((this.AgingFactor == null) || (this.AgingFactor.Value == 0)
                // vagy ha ezt a kor kategóriát még nem érte el
                || (age < this.FromAge))
                return 0;

            var yearsInThisCategory = GetYearsInThisCategory(age);

            // kiszámoljuk hányszor telt már el a korkategóriához tartozó öregedési próba időintervalluma
            var multiplier = yearsInThisCategory / this.AgingFactor.Value;

            return multiplier;
        }

        /// <summary>
        /// Kiszámolja, hogy egy adott korú karakter mennyi Sp-t kap ebben a korkategóriában.
        /// </summary>
        /// <param name="age">A karakter kora, amire a módosítót ki kell számolni.</param>
        /// <returns></returns>
        public int GetFreeSp(decimal age)
        {
            // ha ebben a korkategóriában nem kap Sp-t, vagy ha ezt a kor kategóriát még nem érte el
            if ((this.SpPerYear == 0) || (age < this.FromAge))
                return 0;

            var yearsInThisCategory = GetYearsInThisCategory(age);

            // az Int32-re kerekítés előtt a kerekítési hibák elkerülésére hozzáadunk 0.01m-t, hogy pl. az évente 3 Sp esetben a 0.99999999999 az végül 1 legyen
            //return (int)((this.SpPerYear * yearsInThisCategory));// + 0.01m);
            return (int)(this.SpPerTimeUnit * yearsInThisCategory / this.SpTimeUnitYears);
        }

        private decimal GetYearsInThisCategory(decimal age)
        {
            var yearsInThisCategory = age - (decimal)this.FromAge + 1m;
            // ha ezt a kategóriát már túlhaladta
            if ((this.ToAge != null) && (this.ToAge.Value < age))
                // akkor a teljes kategória időtartamára számolunk
                yearsInThisCategory = this.ToAge.Value + 1 - this.FromAge;

            return yearsInThisCategory;
        }
    }
}
