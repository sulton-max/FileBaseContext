using System.Collections;

namespace FileBaseContext.Set.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    public IEnumerator<TEntity> GetEnumerator() => _entries.Select(entry => entry.Entity).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _entries.Select(entry => entry.Entity).GetEnumerator();
}