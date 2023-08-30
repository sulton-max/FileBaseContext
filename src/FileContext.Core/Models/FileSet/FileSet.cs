using System.Collections;
using System.Linq.Expressions;
using FileContext.Abstractions.Models.Entity;
using FileContext.Abstractions.Models.FileSet;

namespace FileContext.Core.Models.FileSet;

public class FileSet<TEntity, TKey> : IFileSet<TEntity, TKey>
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

    public ValueTask<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<TEntity>> FindRangeAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}