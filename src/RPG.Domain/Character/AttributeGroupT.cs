using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RPG.Domain.Character
{
    public class AttributeGroup<T> : IEnumerable<AttributeBase>
        where T : AttributeBase
    {
        private readonly Dictionary<string, T> attributes;

        public AttributeGroup(string code)
        {
            this.Code = code ?? throw new ArgumentNullException(nameof(code));
            this.attributes = new Dictionary<string, T>(StringComparer.InvariantCulture);
        }

        public AttributeGroup(string code, int capacity)
        {
            this.Code = code ?? throw new ArgumentNullException(nameof(code));
            this.attributes = new Dictionary<string, T>(capacity, StringComparer.InvariantCulture);
        }

        public AttributeGroup(string code, IDictionary<string, T> valuesByCode)
        {
            this.Code = code ?? throw new ArgumentNullException(nameof(code));
            this.attributes = new Dictionary<string, T>(valuesByCode, StringComparer.InvariantCulture);
        }

        [Required]
        public string Code { get; private set; }

        public IEnumerator<AttributeBase> GetEnumerator()
        {
            return this.attributes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
