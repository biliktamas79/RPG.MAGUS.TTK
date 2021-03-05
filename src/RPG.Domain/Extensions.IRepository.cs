using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain
{
    /// <summary>
    /// Static class containing extensions for interfaces
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the entity with the given code. If not found then either null is returned or a <see cref="KeyNotFoundException"/> gets thrown, based on <paramref name="throwIfNotFound"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity of the repository.</typeparam>
        /// <param name="repo">The repository.</param>
        /// <param name="code">The code that is unique within the repository.</param>
        /// <param name="throwIfNotFound">Boolean value indicating whether to throw an exception or to return the type default value if not found.</param>
        /// <returns>Returns the entity if found, or the type default value if not.</returns>
        public static TEntity GetByCode<TEntity>(this IReadOnlyRepository<TEntity> repo, string code, bool throwIfNotFound = true)
            where TEntity : IHasUniqueCode
        {
            if (repo == null)
                throw new ArgumentNullException(nameof(repo));
            if (code == null)
                throw new ArgumentNullException(nameof(code));

            if (repo.TryGetByCode(code, out var value))
            {
                return value;
            }
            else if (throwIfNotFound)
            {
                throw new KeyNotFoundException($"Entity of type '{typeof(TEntity)}' not found by code '{code}'.");
            }

            return default;
        }
    }
}
