using FileContext.Abstractions.Models.FileEntry;
using FileContext.Abstractions.Models.FileSet;
using FileContext.Core.Services;
using Newtonsoft.Json;

namespace FileContext.Core.Models.FileSet;

internal partial class FileSet<TEntity, TKey>
{
    private readonly string _filePath;
    private readonly JsonSerializer _serializer;
    private readonly IPluralizationProvider _pluralizationProvider;
    private readonly List<FileEntityEntry<TEntity>> _entities = new();

    public FileSet(string folderPath, JsonSerializer serializer, IPluralizationProvider? pluralizationProvider)
    {
        _serializer = serializer;
        _pluralizationProvider = pluralizationProvider ?? new HumanizerPluralizationProvider();
        (_filePath, _serializer) = (GetFilePath(folderPath), JsonSerializer.CreateDefault());
    }

    public async ValueTask FetchAsync()
    {
        await foreach (var entity in ReadAsync())
            _entities.Add(new FileEntityEntry<TEntity>(entity));
    }

    public async ValueTask SyncAsync()
    {
        var entitiesToSync = _entities.Where(entity => entity.State != FileEntityState.Deleted)
            .Select(entry => entry.Data);

        await WriteAsync(entitiesToSync);
    }
}