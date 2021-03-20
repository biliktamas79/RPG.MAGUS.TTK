using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Domain;

namespace MAGUS.TTK.Data
{
    /// <summary>
    /// Class that implements <see cref="IRepository{TEntity}"/> by using a <see cref="SortedDictionary{string, TEntity}"/> internally.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InMemoryRepository<TEntity> : IRepository<TEntity>, IEnumerable<TEntity>
        where TEntity : IHasUniqueCode
    {
        private readonly Dictionary<string, TEntity> Dict = new Dictionary<string, TEntity>(StringComparer.CurrentCultureIgnoreCase);

        /// <summary>
        /// Initializes this in-memory repository with the given entities.
        /// </summary>
        /// <param name="values"></param>
        public void Init(IEnumerable<TEntity> entities)
        {
            this.Dict.Clear();

            foreach (var entity in entities)
            {
                this.Dict.Add(entity.Code, entity);
            }
        }

        /// <inheritdoc/>
        public bool TryGetByCode(string code, out TEntity value)
        {
            return this.Dict.TryGetValue(code, out value);
        }

        /// <inheritdoc/>
        public int Count(Func<TEntity, bool> match = null)
        {
            return (match == null)
                ? this.Dict.Count
                : this.Dict.Count(kvp => match(kvp.Value));
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> List(Func<TEntity, bool> match = null)
        {
            return (match == null)
                ? this.Dict.Select(kvp => kvp.Value)
                : this.Dict.Where(kvp => match(kvp.Value)).Select(kvp => kvp.Value);
        }

        /// <inheritdoc/>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Dict.Select(kvp => kvp.Value).GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Dict.Select(kvp => kvp.Value).GetEnumerator();
        }

        public override string ToString()
        {
            return $"InMemoryRepository<{typeof(TEntity).Name}> (Count = {this.Dict.Count})";
        }
    }
}
