namespace FileContext.Abstractions.Models.FileSet;

public partial interface IFileSet<TEntity, in TKey>
{
    /// <summary>
    /// Fetches data from the file system
    /// </summary>
    /// <returns></returns>
    ValueTask FetchAsync();

    /// <summary>
    /// Synchronizes data to the file system
    /// </summary>
    /// <returns></returns>
    ValueTask SyncAsync();
}