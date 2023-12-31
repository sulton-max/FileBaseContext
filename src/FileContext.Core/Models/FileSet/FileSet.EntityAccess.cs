﻿using FileContext.Abstractions.Models.Entity;

namespace FileContext.Core.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    public void Update(TEntity entity, TEntity updated)
    {
        var updatableProperties = typeof(TEntity).GetProperties()
            .Where(property => property.Name != nameof(IFileSetEntity<TKey>.Id))
            .ToList();

        updatableProperties.ForEach(property => property.SetValue(entity, property.GetValue(updated)));
    }
}