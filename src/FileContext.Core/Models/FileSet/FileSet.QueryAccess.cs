namespace FileBaseContext.Set.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    // public Type ElementType { get; }
    // public Expression Expression { get; }
    // public IQueryProvider Provider { get; }
    //
    // public IEnumerator<TEntity> GetEnumerator() => _entries.Select(entry => entry.Entity).GetEnumerator();
    //
    // IEnumerator IEnumerable.GetEnumerator() => _entries.Select(entry => entry.Entity).GetEnumerator();
    //
    // public IEnumerable<Task<TEntity>> EnumerateAsTask() => _entries.Select(entity => Task.FromResult(entity.Entity));
    //
    // public async IAsyncEnumerable<TEntity> EnumerateAsync()
    // {
    //     foreach (var entity in EnumerateAsTask())
    //         yield return await entity;
    // }
    //
    // public IAsyncEnumerator<TEntity>
    //     GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) =>
    //     EnumerateAsync().GetAsyncEnumerator(cancellationToken);
}