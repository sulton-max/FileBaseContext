using Newtonsoft.Json.Linq;

namespace FileBaseContext.Abstractions.Models.FileSet;

public partial interface IFileSet<TEntity, in TKey>
{
    /// <summary>
    /// Calculates the file path for the given entity
    /// </summary>
    /// <param name="folderPath">Folder path where files will be created</param>
    /// <returns>Absolute file path</returns>
    string GetFilePath(string folderPath);

    /// <summary>
    /// Reads all entities from the file base asynchronously
    /// </summary>
    /// <returns>Stream of entities</returns>
    IAsyncEnumerable<TEntity> ReadAsync();

    /// <summary>
    /// Writes all entities to the file base
    /// </summary>
    /// <param name="entities">Entities to write</param>
    /// <returns>     A task that represents the asynchronous Write operation</returns>
    ValueTask WriteAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///
    /// </summary>
    /// <param name="token"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool IsValidEntry(JToken token, out TEntity? entity);
}