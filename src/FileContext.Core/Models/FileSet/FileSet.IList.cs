using FileContext.Abstractions.Models.FileEntry;
using FileContext.Core.Extensions;

namespace FileContext.Core.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    private ValueTask<FileEntityEntry<TEntity>?> FindEntryAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<FileEntityEntry<TEntity>?>(
            Task.Run(() => _entries.FirstOrDefault(entity => entity.Entity.Id.Equals(id)), cancellationToken));
    }

    private ValueTask<IList<FileEntityEntry<TEntity>>> FindEntryRangeAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<IList<FileEntityEntry<TEntity>>>(Task.Run(() =>
                (IList<FileEntityEntry<TEntity>>)_entries.Where(entity => ids.Contains(entity.Entity.Id)).ToList(),
            cancellationToken));
    }

    public ValueTask<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<TEntity?>(
            Task.Run(() => _entries.FirstOrDefault(entity => entity.Entity.Id.Equals(id))?.Entity, cancellationToken));
    }

    public async ValueTask<IList<TEntity>> FindRangeAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default
    )
    {
        return (await FindEntryRangeAsync(ids, cancellationToken)).Select(entity => entity.Entity).ToList();
    }

    public ValueTask<FileEntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return new ValueTask<FileEntityEntry<TEntity>>(Task.Run(() =>
            {
                _entries.Add(new FileEntityEntry<TEntity>(entity));
                return _entries.Last();
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
                _entries.AddRange(entities.Select(entity => new FileEntityEntry<TEntity>(entity)));
                return _entries.TakeLast(entities.Count());
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
                // var foundEntity = _entries.FirstOrDefault(e => e.Entity.Id.Equals(entity.Id));
                var foundEntry = await FindEntryAsync(entity.Id, cancellationToken);
                // TODO : Add custom exceptions

                Update(foundEntry.Entity, entity);
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
                // var zip = foundEntries.Select(entity => entity.Entity).ZipIntersectBy(entities, entity => entity.Id);
                var zip = foundEntries.Select(entity => entity.Entity).ZipIntersectBy(entities, entity => entity.Id);

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
                // var foundEntity = _entries.FirstOrDefault(e => e.Entity.Id.Equals(entity.Id));
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
                var intersect =
                    foundEntries.IntersectBy(entities.Select(entity => entity.Id), entity => entity.Entity.Id);

                foreach (var entry in intersect)
                    entry.State = FileEntityState.Deleted;

                return foundEntries;
            },
            cancellationToken));
    }

    public ValueTask<IList<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
    {
        return new ValueTask<IList<TEntity>>(
            Task.Run(() => (IList<TEntity>)_entries.Select(entity => entity.Entity).ToList(), cancellationToken));
    }
}