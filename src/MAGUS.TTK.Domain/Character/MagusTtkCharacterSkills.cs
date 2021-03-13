using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS TTK karakter képzettségei
    /// </summary>
    public class MagusTtkCharacterSkills : IEnumerable<SkillLevel>
    {
        private List<SkillLevel> SkillLevels { get; set; }

        ///// <summary>
        ///// Levels os non-specializable skills by skill codes.
        ///// </summary>
        //public readonly Dictionary<string, SkillLevel> SimpleSkillLevels = new Dictionary<string, SkillLevel>();
        ///// <summary>
        ///// Levels of specializable skills by specializations by skill codes.
        ///// </summary>
        //public readonly Dictionary<string, Dictionary<string, SkillLevel>> SpecializableSkillLevels = new Dictionary<string, Dictionary<string, SkillLevel>>();

        ///// <summary>
        ///// Gets the count of skills
        ///// </summary>
        //public int Count
        //{
        //    get { return this.SimpleSkillLevels.Count + this.SpecializableSkillLevels.Count; }
        //}

        ///// <summary>
        ///// Gets the count of skills
        ///// </summary>
        //public int CountIncludingSpecializations
        //{
        //    get
        //    {
        //        var ret = this.SimpleSkillLevels.Count;

        //        foreach (var kvp in this.SpecializableSkillLevels)
        //        {
        //            if (kvp.Value != null)
        //                ret += kvp.Value.Count;
        //        }

        //        return ret;
        //    }
        //}

        ///// <summary>
        ///// Checks whether the skill with the given code exists in this skill collection.
        ///// </summary>
        ///// <param name="skillCode"></param>
        ///// <returns></returns>
        //public bool HasSkill(string skillCode)
        //{
        //    return this.SimpleSkillLevels.ContainsKey(skillCode) || this.SpecializableSkillLevels.ContainsKey(skillCode);
        //}

        ///// <summary>
        ///// Checks whether the skill with the given code and specialization exists in this skill collection.
        ///// </summary>
        ///// <param name="skillCode"></param>
        ///// <param name="specialization"></param>
        ///// <returns></returns>
        //public bool HasSkillSpecialization(string skillCode, string specialization)
        //{
        //    return this.SpecializableSkillLevels.TryGetValue(skillCode, out var specializationDict)
        //        && (specializationDict?.ContainsKey(specialization) ?? false);
        //}

        /// <summary>
        ///  Gets the sum Kp cost of all skills in this skill collection.
        /// </summary>
        /// <returns></returns>
        public int GetKpCost()
        {
            int sumKp = 0;

            //foreach (var simpleSkill in SimpleSkillLevels.Values)
            //{
            //    if (simpleSkill == null)
            //        continue;

            //    sumKp += simpleSkill.Definition.SkillClassDefinition.GetKpCostOfLevel(simpleSkill.Level);
            //}

            //foreach (var specDict in this.SpecializableSkillLevels.Values)
            //{
            //    if (specDict == null)
            //        continue;

            //    foreach (var specSkill in specDict.Values)
            //    {
            //        if (specSkill == null)
            //            continue;

            //        sumKp += specSkill.Definition.SkillClassDefinition.GetKpCostOfLevel(specSkill.Level);
            //    }
            //}

            if (this.SkillLevels != null)
            {
                foreach (var sl in this.SkillLevels)
                {
                    sumKp += sl.Definition.SkillClassDefinition.GetKpCostOfLevel(sl.Level);
                }
            }

            return sumKp;
        }

        public void Initialize(IEnumerable<SkillLevel> initialSkillLevels)
        {
            this.SkillLevels = new List<SkillLevel>(initialSkillLevels);
        }

        public IEnumerator<SkillLevel> GetEnumerator()
        {
            return this.SkillLevels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.SkillLevels.GetEnumerator();
        }
    }
}
