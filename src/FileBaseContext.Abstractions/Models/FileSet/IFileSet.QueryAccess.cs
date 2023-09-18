namespace FileBaseContext.Abstractions.Models.FileSet;

public partial interface IFileSet<TEntity, in TKey>
{
    // /// <summary>
    // /// Creates an enumerable of all entities from the context
    // /// </summary>
    // /// <returns>Enumerable collection of entity as task</returns>
    // IEnumerable<Task<TEntity>> EnumerateAsTask();
}