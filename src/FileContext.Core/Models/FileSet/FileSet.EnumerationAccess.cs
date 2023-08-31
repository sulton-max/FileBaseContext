using System.Collections;

namespace FileContext.Core.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    public IEnumerator<TEntity> GetEnumerator() => _entries.Select(entry => entry.Entity).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _entries.Select(entry => entry.Entity).GetEnumerator();
}