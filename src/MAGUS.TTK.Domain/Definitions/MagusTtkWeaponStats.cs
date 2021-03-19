using RPG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// Egy MAGUS rendszerbeli fegyver harci statisztikái
    /// </summary>
    public class MagusTtkWeaponStats
    {
        /// <summary>
        /// A fegyver támadó értéke.
        /// </summary>
        public int TE { get; set; }
        /// <summary>
        /// A fegyver védő értéke.
        /// </summary>
        public int VE { get; set; }
        /// <summary>
        /// A fegyver célzó értéke.
        /// </summary>
        public int CE { get; set; }
        /// <summary>
        /// A fegyver méterben megadott optimális lő- illetve dobó távolsága.
        /// </summary>
        public int Tav { get; set; }
        /// <summary>
        /// A fegyver sebzés kódja.
        /// </summary>
        public string Seb { get; set; }
        /// <summary>
        /// A fegyverrel harci hátrány jelentkezése nélkül leadható körönkénti támadások száma.
        /// </summary>
        public int Tam { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.TE != 0)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(" TÉ: ").Append(this.TE);
            }
            if (this.VE != 0)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(" VÉ: ").Append(this.VE);
            }
            if (this.CE != 0)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(" CÉ: ").Append(this.CE);
            }
            if (this.Tav != 0)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(" Táv: ").Append(this.Tav);
            }
            if (sb.Length > 0)
                sb.Append(" ");
            sb.Append("Seb: ").Append(this.Seb);
            sb.Append(" Tám: ").Append(this.Tam);
            
            return sb.ToString();
        }
    }
}
