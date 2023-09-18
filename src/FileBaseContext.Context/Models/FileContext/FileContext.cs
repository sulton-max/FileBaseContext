using System.Data;
using System.Reflection;
using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileContext;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Extensions;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Set.Models.FileSet;
using FileBaseContext.Set.Services;
using Newtonsoft.Json;

namespace FileBaseContext.Context.Models.FileContext;

public abstract class FileContext : IFileContext
{
    private readonly string _rootFolderPath;
    private readonly JsonSerializer _jsonSerializer = new();
    private readonly IPluralizationProvider _pluralizationProvider = new HumanizerPluralizationProvider();
    private readonly Dictionary<string, IFileSetBase> _fileSets;

    public event Func<IEnumerable<IFileSetBase>, ValueTask> OnSaveChanges;

    protected FileContext(IFileContextOptions<IFileContext> fileContextOptions)
    {
        // Set root path
        _rootFolderPath = fileContextOptions.StorageRootPath;

        // Get file sets
        var fileSets = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(fileSet => fileSet.PropertyType.InheritsOrImplements(typeof(IFileSetBase)))
            .Select(fileSet => (fileSet.Name,
                Value: fileSet.GetValue(this) as IFileSetBase ?? throw new EvaluateException("File set is not initialized")))
            .Select(fileSet => new KeyValuePair<string, IFileSetBase>(fileSet.Name, fileSet.Value));
        _fileSets = new Dictionary<string, IFileSetBase>(fileSets);

        // Fetch if it's required
        if (fileContextOptions.FetchOnContextCreation)
            FetchAsync().AsTask().Wait();
    }

    public virtual IFileSet<TEntity, Guid> Set<TEntity>(string fileSetName) where TEntity : class, IFileSetEntity<Guid>
    {
        _fileSets.TryAdd(fileSetName, new FileSet<TEntity, Guid>(_rootFolderPath, _jsonSerializer, _pluralizationProvider));
        return (IFileSet<TEntity, Guid>)_fileSets[fileSetName];
    }

    public virtual async ValueTask FetchAsync()
    {
        await Task.WhenAll(_fileSets.Values.Select(fileSet => fileSet.FetchAsync().AsTask()));
    }

    public virtual async ValueTask SaveChangesAsync()
    {
        await OnSaveChanges(_fileSets.Values);
        await Task.WhenAll(_fileSets.Values.Select(fileSet => fileSet.SyncAsync().AsTask()));
    }
}