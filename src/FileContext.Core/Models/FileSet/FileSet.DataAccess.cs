using FileBaseContext.Abstractions.Models.FileEntry;

namespace FileBaseContext.Set.Models.FileSet;

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
        _entries.RemoveAll(entry => entry.State == FileEntityState.Deleted);
        await WriteAsync(_entries.Select(entry => entry.Entity));
    }
}