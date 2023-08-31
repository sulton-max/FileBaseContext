using FileContext.Abstractions.Models.Entity;
using FileContext.Abstractions.Models.FileSet;

namespace FileContext.Core.Models.FileSet;

/// <summary>
/// Represents a collection unit backed by a file
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
internal partial class FileSet<TEntity, TKey> : IFileSet<TEntity, TKey>
    where TEntity : class, IFileSetEntity<TKey> where TKey : struct
{
    public ValueTask SaveChangesAsync(CancellationToken cancellationToken = default) => SyncAsync();
}