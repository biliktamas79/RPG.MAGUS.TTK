using System;
using System.ComponentModel.DataAnnotations;

namespace RPG.Domain.Character
{
    public abstract class AttributeBase : IHasUniqueCode
    {
        public AttributeBase(string code)
        {
            this.Code = code ?? throw new ArgumentNullException(nameof(code));
        }

        [Required]
        public string Code { get; private set; }
    }
}
