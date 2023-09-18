namespace FileBaseContext.FileSet.Extensions;

internal static class LinqExtensions
{
    internal static IEnumerable<(TSource firstItem, TSource secondItem)> ZipIntersectBy<TSource, TKey>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector
    )
    {
        return from firstItem in first
            let key = keySelector(firstItem)
            let secondItem = second.FirstOrDefault(secondItem => keySelector(secondItem)?.Equals(key) ?? false)
            where firstItem is not null && secondItem is not null && firstItem.GetHashCode() != secondItem.GetHashCode()
            select (firstItem, secondItem);
    }
}