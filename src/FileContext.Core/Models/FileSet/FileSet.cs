using System.Linq.Expressions;
using FileContext.Abstractions.Models.Entity;
using FileContext.Abstractions.Models.FileSet;
using FileContext.Core.Services;
using Newtonsoft.Json;

namespace FileContext.Core.Models.FileSet;

/// <summary>
/// Represents a collection unit backed by a file
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
public partial class FileSet<TEntity, TKey> : IFileSet<TEntity, TKey>
    where TEntity : class, IFileSetEntity<TKey> where TKey : struct
{
    public FileSet(string folderPath, JsonSerializer serializer, IPluralizationProvider? pluralizationProvider)
    {
        _serializer = serializer;
        _pluralizationProvider = pluralizationProvider ?? new HumanizerPluralizationProvider();
        (_filePath, _serializer) = (GetFilePath(folderPath), JsonSerializer.CreateDefault());
        ElementType = typeof(TEntity);

        // TODO : implement query provider
    }
    
    public ValueTask SaveChangesAsync(CancellationToken cancellationToken = default) => SyncAsync();
}