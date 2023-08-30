using FileContext.Abstractions.Models.Entity;

namespace FileContext.Abstractions.Models.FileSet;

/// <summary>
///     A <see cref="IFileSet{TEntity,TKey}" /> can be used to query and save instances of <typeparamref name="TEntity" />.
///     LINQ queries against a <see cref="IFileSet{TEntity,TKey}" /> will be translated direct read/write against the file system
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
public interface IFileSet<TEntity, in TKey> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>
    where TKey : struct where TEntity : class, IFileSetEntity<TKey>
{
    /// <summary>
    /// Finds an entity with the given primary key values. If an entity with the given is being tracked by the context, then it is returned immediately without making a request to the file base
    /// </summary>
    /// <param name="id">Primary key of entity</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Find operation. The task result contains the
    ///     <see cref="TEntity" /> for the entity.
    /// </returns>
    ValueTask<TEntity?> FindAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds entities with the given primary key values. If an entity with the given is being tracked by the context, then it is returned immediately without making a request to the file base
    /// </summary>
    /// <param name="ids">Primary keys of entities</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Find operation. The task result contains the
    ///     <see cref="IEnumerable{TEntity}" /> for the entity.
    /// </returns>
    ValueTask<IEnumerable<TEntity>> FindRangeAsync(
        IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Adds the given entity to the context
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The entity found, or <see langword="null" />.</returns>
    /// <returns>
    ///     A task that represents the asynchronous Add operation. The task result contains the
    ///     <see cref="TEntity" /> for the entity.
    /// </returns>
    ValueTask<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the given entities to the context
    /// </summary>
    /// <param name="entities">The entities to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Add operation. The task result contains the
    ///     <see cref="IEnumerable{TEntity}" /> for the entity.
    /// </returns>
    ValueTask<IEnumerable<TEntity>> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Updates the given entity in the context
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Update operation. The task result contains the
    ///     <see cref="TEntity" /> for the entity.
    /// </returns>
    ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the given entities in the context
    /// </summary>
    /// <param name="entities">The entities to update</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Update operation. The task result contains the
    ///     <see cref="IEnumerable{TEntity}" /> for the entity.
    /// </returns>
    ValueTask<IEnumerable<TEntity>> UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Removes the given entity in the context
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Remove operation. The task result contains the
    ///     <see cref="TEntity" /> for the entity.
    /// </returns>
    ValueTask<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the given entities in the context
    /// </summary>
    /// <param name="entities">The entities to remove</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Remove operation. The task result contains the
    ///     <see cref="IEnumerable{TEntity}" /> for the entity.
    /// </returns>
    ValueTask<IEnumerable<TEntity>> RemoveRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Saves all changes made in this context to the file base
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous Update operation
    /// </returns>
    internal ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
}