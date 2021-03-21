using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public Task<bool> ExistsByCode(string code, CancellationToken cancellationToken = default)
        {
            var result = this.Dict.ContainsKey(code);

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<bool> TryGetByCode(string code, out TEntity value, CancellationToken cancellationToken = default)
        {
            var result = this.Dict.TryGetValue(code, out value);

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<int> Count(Func<TEntity, bool> match = null, CancellationToken cancellationToken = default)
        {
            var result = (match == null)
                ? this.Dict.Count
                : this.Dict.Count(kvp => match(kvp.Value));

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> All(Func<TEntity, bool> match = null, CancellationToken cancellationToken = default)
        {
            var result = (match == null)
                ? this.Dict.Select(kvp => kvp.Value)
                : this.Dict.Where(kvp => match(kvp.Value)).Select(kvp => kvp.Value);

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Dict.Add(entity.Code, entity);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                cancellationToken.ThrowIfCancellationRequested();

                this.Dict.Add(entity.Code, entity);
            }

            return Task.CompletedTask;
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
