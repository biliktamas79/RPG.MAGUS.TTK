using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using RPG.Domain.Definitions;

namespace MAGUS.TTK.Domain.Character
{
    /// <summary>
    /// MAGUS karakter képesség, mint Erő, Ügy, ...
    /// </summary>
    public class AbilityCompoundValue
    {
        /// <summary>
        /// Tulajdonság definíció
        /// </summary>
        [JsonIgnoreAttribute]
        public AbilityDefinition Definition { get; set; }

        /// <summary>
        /// Tulajdonság módosítók típus szerint
        /// </summary>
        public readonly Dictionary<AbilityValueComponentTypeEnum, AbilityValueComponent> ValueComponentsByType = new Dictionary<AbilityValueComponentTypeEnum, AbilityValueComponent>(5);

        /// <summary>
        /// A tulajdonság értéke
        /// </summary>
        public int Value
        {
            get { return this.ValueComponentsByType.Sum(kvp => kvp.Value?.Value ?? 0); }
        }

        public override string ToString()
        {
            return $"{this.Definition.Code}: {this.Value}";
        }

        public void Init()
        {
            SetComponentValue(AbilityValueComponentTypeEnum.CharacterCreation, 3);
            SetComponentValue(AbilityValueComponentTypeEnum.Class, this.Definition.MinValue);
            SetComponentValue(AbilityValueComponentTypeEnum.Race, 0);
            SetComponentValue(AbilityValueComponentTypeEnum.LevelUp, 0);
            SetComponentValue(AbilityValueComponentTypeEnum.BoughtFromSP, 0);
        }

        public void SetComponentValue(AbilityValueComponentTypeEnum type, int value)
        {
            //if (value < this.Definition.MinValue)
            //    throw new ArgumentOutOfRangeException(nameof(value), $"Value '{value}' of component with type '{type}' of ability '{this.Definition.Name ?? this.Definition.Code}' is below the minimum '{this.Definition.MinValue}'.");

            if (!this.ValueComponentsByType.TryGetValue(type, out var component) || (component == null))
                this.ValueComponentsByType[type] = new AbilityValueComponent(type) { Value = value };
            else
                component.Value = value;
        }

        public AbilityValueComponent GetValueComponentOrThrow(AbilityValueComponentTypeEnum type)
        {
            if (!this.ValueComponentsByType.TryGetValue(type, out var avc) || (avc == null))
                throw new ArgumentException($"Ability value component not found with type '{type}'.");
            
            return avc;
        }

        //public IntValueWithDiceModifierDto GetValueWithDiceModifier()
        //{
        //    int val = this.Value;
        //    int? diceModifier = (val % 2 == 0) ? null : 2;
        //    return new IntValueWithDiceModifierDto() { Value = val / 2, DiceModifier = diceModifier };
        //}

        public int? GetDiceModifier()
        {
            return (this.Value % 2 == 0) ? null : 2;
        }

        public string GetDiceModifierString()
        {
            return (this.Value % 2 == 0) ? null : "+2";
        }

        public string GetDiceRollValueString()
        {
            int val = this.Value;
            return (val % 2 == 0)
                ? $"{val / 2}"
                : $"{val / 2} (+2)";
        }

        public int GetSumValue()
        {
            return this.Value;
        }
    }
}
