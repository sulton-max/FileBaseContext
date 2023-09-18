namespace FileBaseContext.Abstractions.Models.FileSet;

public partial interface IFileSet<TEntity, in TKey>
{
    /// <summary>
    /// Updates entity values with the values from the updated entity
    /// </summary>
    /// <param name="entity">Entity in the context</param>
    /// <param name="updated">Updated entity</param>
    void Update(TEntity entity, TEntity updated);
}