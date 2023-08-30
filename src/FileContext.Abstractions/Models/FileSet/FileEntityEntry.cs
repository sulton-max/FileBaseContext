namespace FileContext.Abstractions.Models.FileSet;

/// <summary>
/// Provides information about entity state in a file base
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public class FileEntityEntry<TEntity>
{
    /// <summary>
    /// The entity being tracked by a context
    /// </summary>
    public TEntity Data { get; set; }

    /// <summary>
    ///  The state in which an entity is being tracked by a context
    /// </summary>
    public FileEntityState State { get; set; }

    public FileEntityEntry(TEntity data) => Data = data;
}