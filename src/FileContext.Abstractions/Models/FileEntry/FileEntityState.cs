namespace FileContext.Abstractions.Models.FileEntry;

/// <summary>
/// The state in which an entity is being tracked by a context.
/// </summary>
public enum FileEntityState
{
    /// <summary>
    ///     The entity is being tracked by the context and exists in the file base. Its property
    ///     values have not changed from the values in the database.
    /// </summary>
    Unchanged = 0,

    /// <summary>
    ///     The entity is being tracked by the context and exists in the file base. It has been marked
    ///     for deletion from the database.
    /// </summary>
    Deleted = 1,

    /// <summary>
    ///     The entity is being tracked by the context and exists in the file base. Some or all of its
    ///     property values have been modified.
    /// </summary>
    Modified = 2,

    /// <summary>
    ///     The entity is being tracked by the context but does not yet exist in the file base.
    /// </summary>
    Added = 3
}