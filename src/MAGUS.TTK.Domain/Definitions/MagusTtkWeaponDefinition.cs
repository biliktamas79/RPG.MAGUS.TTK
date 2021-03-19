using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using MAGUS.TTK.Domain.Definitions;
using RPG.Domain;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Definitions
{
    /// <summary>
    /// MAGUS TTK fegyver definíció.
    /// </summary>
    public class MagusTtkWeaponDefinition : IHasUniqueCode
    {
        /// <summary>
        /// fegyver kód
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Fegyver kategória kódok, amikkel használható
        /// </summary>
        public string[] CategoryCodes { get; set; }
        /// <summary>
        /// A fegyver neve
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Weapon description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A fegyver MGT-je
        /// </summary>
        public int MGT { get; set; }
        /// <summary>
        /// Egy kézben forgatva a fegyver statisztikai értékei.
        /// </summary>
        public MagusTtkWeaponStats StatsIn1Hand { get; set; }
        /// <summary>
        /// Két kézben forgatva a fegyver statisztikai értékei.
        /// </summary>
        public MagusTtkWeaponStats StatsIn2Hands { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Code);
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                sb.Append(" (").Append(this.Name).Append(")");
            }
            if (this.MGT != 0)
            {
                sb.Append(" MGT: ").Append(this.MGT);
            }
            if (this.StatsIn1Hand != null)
                sb.Append(" 1 kézben: [").Append(this.StatsIn1Hand).Append("]");
            if (this.StatsIn2Hands != null)
                sb.Append(" 2 kézben: [").Append(this.StatsIn2Hands).Append("]");

            return sb.ToString();
        }
    }
}
