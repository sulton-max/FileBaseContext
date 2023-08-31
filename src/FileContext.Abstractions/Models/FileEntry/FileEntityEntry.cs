namespace FileContext.Abstractions.Models.FileEntry;

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

    /// <summary>
    /// Instantiates a new <see cref="FileEntityEntry{TEntity}"/> with <see cref="FileEntityState.Unchanged" /> state
    /// </summary>
    /// <param name="data">initial entity data</param>
    public FileEntityEntry(TEntity data) => Data = data;
}