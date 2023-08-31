using FileContext.Abstractions.Models.FileEntry;
using FileContext.Abstractions.Models.FileSet;
using FileContext.Core.Services;
using Newtonsoft.Json;

namespace FileContext.Core.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    private readonly string _filePath;
    private readonly List<FileEntityEntry<TEntity>> _entries = new();

    public async ValueTask FetchAsync()
    {
        await foreach (var entity in ReadAsync())
            _entries.Add(new FileEntityEntry<TEntity>(entity));
    }

    public async ValueTask SyncAsync()
    {
        var entitiesToSync = _entries.Where(entity => entity.State != FileEntityState.Deleted)
            .Select(entry => entry.Entity);

        await WriteAsync(entitiesToSync);
    }
}