using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG.Domain
{
    /// <summary>
    /// Generic interface of read-only entity repositories.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that must implement the <see cref="IHasUniqueCode"/> interface.</typeparam>
    public interface IReadOnlyRepository<TEntity>
        where TEntity : IHasUniqueCode
    {
        /// <summary>
        /// Checks whether an entity with the given code already exists.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns true if an entity with the given code already exists, otherwise false.</returns>
        Task<bool> ExistsByCode(string code, CancellationToken cancellationToken = default);
        /// <summary>
        /// Tries to get the entity by the given code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="value">The value if found, otherwise the type's default value.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns true if the entity was found, otherwise false.</returns>
        Task<bool> TryGetByCode(string code, out TEntity value, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the count of entities matching the given optional filter.
        /// </summary>
        /// <param name="match">The optional filter to match.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the count of entities matching the given filter.</returns>
        Task<int> Count(Func<TEntity, bool> match = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the entities matching the given optional filter.
        /// </summary>
        /// <param name="match">The optional filter to match.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the entities matching the given filter.</returns>
        Task<IEnumerable<TEntity>> All(Func<TEntity, bool> match = null, CancellationToken cancellationToken = default);
    }
}
