using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.Domain
{
    /// <summary>
    /// Generic interface of entity repositories.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that must implement the <see cref="IHasUniqueCode"/> interface.</typeparam>
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : IHasUniqueCode
    {
        ///// <summary>
        ///// Tries to get the entity by the given code.
        ///// </summary>
        ///// <param name="code">The code.</param>
        ///// <param name="value">The value if found, otherwise the type's default value.</param>
        ///// <returns>Returns true if the entity was found, otherwise false.</returns>
        //bool TryGetByCode(string code, out TEntity value);

        ///// <summary>
        ///// Gets the count of entities matching the given optional filter.
        ///// </summary>
        ///// <param name="match">The optional filter to match.</param>
        ///// <returns>Returns the count of entities matching the given filter.</returns>
        //int Count(Func<TEntity, bool> match = null);

        ///// <summary>
        ///// Gets the entities matching the given optional filter.
        ///// </summary>
        ///// <param name="match">The optional filter to match.</param>
        ///// <returns>Returns the entities matching the given filter.</returns>
        //IEnumerable<TEntity> List(Func<TEntity, bool> match = null);
    }
}
