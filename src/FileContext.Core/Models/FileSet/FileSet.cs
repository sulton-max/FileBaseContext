using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.FileSet.Services;
using Newtonsoft.Json;

namespace FileBaseContext.FileSet.Models.FileSet;

/// <summary>
/// Represents a collection unit backed by a file
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TKey">Entity primary key type</typeparam>
public partial class FileSet<TEntity, TKey> : IFileSet<TEntity, TKey>
    where TEntity : class, IFileSetEntity<TKey> where TKey : struct
{
    public FileSet(string folderPath, JsonSerializer? serializer, IPluralizationProvider? pluralizationProvider)
    {
        _serializer = serializer ?? JsonSerializer.CreateDefault();
        _pluralizationProvider = pluralizationProvider ?? new HumanizerPluralizationProvider();
        _filePath = GetFilePath(folderPath);
        _serializer = JsonSerializer.CreateDefault();

        // TODO : implement query provider
    }

    public ValueTask SaveChangesAsync(CancellationToken cancellationToken = default) => SyncAsync();
}