using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileSet;

namespace FileBaseContext.Abstractions.Models.FileContext;

/// <summary>
/// Defines the data context essential methods
/// </summary>
public interface IFileContext
{
    /// <summary>
    /// Configures <see cref="IFileSet{TEntity,TKey}"/> with internal configurations
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <returns>Initialized and configured file set</returns>
    IFileSet<TEntity, Guid> Set<TEntity>(string fileSetName) where TEntity : class, IFileSetEntity<Guid>;

    /// <summary>
    ///  Fetches all entities from the file base
    /// </summary>
    /// <returns>A task that represents the asynchronous fetch operation</returns>
    ValueTask FetchAsync();

    /// <summary>
    /// Saves current context changes to the file base
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation</returns>
    ValueTask SaveChangesAsync();
}