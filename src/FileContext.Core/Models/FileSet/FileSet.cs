using System.Collections;
using System.Linq.Expressions;
using FileContext.Abstractions.Models.Entity;
using FileContext.Abstractions.Models.FileEntry;
using FileContext.Abstractions.Models.FileSet;
using FileContext.Core.Extensions;

namespace FileContext.Core.Models.FileSet;

/// <summary>
/// Represents a collection unit backed by a file
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
internal partial class FileSet<TEntity, TKey> : IFileSet<TEntity, TKey>
    where TEntity : class, IFileSetEntity<TKey> where TKey : struct
{
    public IEnumerator<TEntity> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Type ElementType { get; }
    public Expression Expression { get; }
    public IQueryProvider Provider { get; }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    private ValueTask<FileEntityEntry<TEntity>?> FindEntryAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<FileEntityEntry<TEntity>?>(
            Task.Run(() => _entities.FirstOrDefault(entity => entity.Data.Id.Equals(id)), cancellationToken));
    }

    private ValueTask<IList<FileEntityEntry<TEntity>>> FindEntryRangeAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<IList<FileEntityEntry<TEntity>>>(Task.Run(() =>
                (IList<FileEntityEntry<TEntity>>)_entities.Where(entity => ids.Contains(entity.Data.Id)).ToList(),
            cancellationToken));
    }

    public ValueTask<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<TEntity?>(
            Task.Run(() => _entities.FirstOrDefault(entity => entity.Data.Id.Equals(id))?.Data, cancellationToken));
    }

    public async ValueTask<IList<TEntity>> FindRangeAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default
    )
    {
        return (await FindEntryRangeAsync(ids, cancellationToken)).Select(entity => entity.Data).ToList();
    }

    public ValueTask<FileEntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return new ValueTask<FileEntityEntry<TEntity>>(Task.Run(() =>
            {
                _entities.Add(new FileEntityEntry<TEntity>(entity));
                return _entities.Last();
            },
            cancellationToken));
    }

    public ValueTask<IEnumerable<FileEntityEntry<TEntity>>> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<IEnumerable<FileEntityEntry<TEntity>>>(Task.Run(() =>
            {
                // TODO : Resolve multiple enumerations
                _entities.AddRange(entities.Select(entity => new FileEntityEntry<TEntity>(entity)));
                return _entities.TakeLast(entities.Count());
            },
            cancellationToken));
    }

    public ValueTask<FileEntityEntry<TEntity>> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<FileEntityEntry<TEntity>>(Task.Run(async () =>
            {
                // var foundEntity = _entities.FirstOrDefault(e => e.Data.Id.Equals(entity.Id));
                var foundEntry = await FindEntryAsync(entity.Id, cancellationToken);
                // TODO : Add custom exceptions

                Update(foundEntry.Data, entity);
                return foundEntry;
            },
            cancellationToken));
    }

    public ValueTask<IList<FileEntityEntry<TEntity>>> UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<IList<FileEntityEntry<TEntity>>>(Task.Run(async () =>
            {
                var foundEntries = await FindEntryRangeAsync(entities.Select(entity => entity.Id), cancellationToken);
                // var zip = foundEntries.Select(entity => entity.Data).ZipIntersectBy(entities, entity => entity.Id);
                var zip = foundEntries.Select(entity => entity.Data).ZipIntersectBy(entities, entity => entity.Id);

                foreach (var (first, second) in zip)
                {
                    // TODO : use entry from found entities
                    var entry = await FindEntryAsync(first.Id, cancellationToken);
                    entry!.State = FileEntityState.Modified;
                    Update(first, second);
                }

                return foundEntries;
            },
            cancellationToken));
    }

    public ValueTask<FileEntityEntry<TEntity>> RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        // remove entity
        return new ValueTask<FileEntityEntry<TEntity>>(Task.Run(async () =>
            {
                // var foundEntity = _entities.FirstOrDefault(e => e.Data.Id.Equals(entity.Id));
                var foundEntity = await FindEntryAsync(entity.Id, cancellationToken);
                // TODO : Add custom exceptions

                foundEntity!.State = FileEntityState.Deleted;
                return foundEntity;
            },
            cancellationToken));
    }

    public ValueTask<IList<FileEntityEntry<TEntity>>> RemoveRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<IList<FileEntityEntry<TEntity>>>(Task.Run(async () =>
            {
                var foundEntries = await FindEntryRangeAsync(entities.Select(entity => entity.Id), cancellationToken);
                var intersect = foundEntries.IntersectBy(entities.Select(entity => entity.Id), entity => entity.Data.Id);

                foreach (var entry in intersect)
                    entry.State = FileEntityState.Deleted;

                return foundEntries;
            },
            cancellationToken));
    }

    public ValueTask<IList<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
    {
        return new ValueTask<IList<TEntity>>(Task.Run(() => (IList<TEntity>)_entities.Select(entity => entity.Data).ToList(), cancellationToken));
    }

    public ValueTask SaveChangesAsync(CancellationToken cancellationToken = default) => SyncAsync();
}