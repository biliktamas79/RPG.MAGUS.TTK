using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the entity if found, or the type default value if not.</returns>
        public static async Task<TEntity> GetByCode<TEntity>(this IReadOnlyRepository<TEntity> repo, string code, bool throwIfNotFound = true, CancellationToken cancellationToken = default)
            where TEntity : IHasUniqueCode
        {
            if (repo == null)
                throw new ArgumentNullException(nameof(repo));
            if (code == null)
                throw new ArgumentNullException(nameof(code));

            if (await repo.TryGetByCode(code, out var value, cancellationToken))
            {
                return value;
            }
            else if (throwIfNotFound)
            {
                throw new KeyNotFoundException($"Entity of type '{typeof(TEntity)}' not found by code '{code}'.");
            }

            return default;
        }

        public static IRepository<TEntity> AsEditableRepository<TEntity>(this IReadOnlyRepository<TEntity> repo)
            where TEntity : IHasUniqueCode
        {
            if (repo == null)
                throw new ArgumentNullException(nameof(repo));

            return (IRepository<TEntity>)repo;
        }

        public static IReadOnlyRepository<TEntity> AsReadOnlyRepository<TEntity>(this IRepository<TEntity> repo)
            where TEntity : IHasUniqueCode
        {
            if (repo == null)
                throw new ArgumentNullException(nameof(repo));

            return (IReadOnlyRepository<TEntity>)repo;
        }
    }
}
