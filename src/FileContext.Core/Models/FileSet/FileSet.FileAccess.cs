using FileBaseContext.FileSet.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FileBaseContext.FileSet.Models.FileSet;

public partial class FileSet<TEntity, TKey>
{
    private readonly JsonSerializer _serializer;
    private readonly IPluralizationProvider _pluralizationProvider;

    public string GetFilePath(string folderPath)
    {
        var lowercaseEntityName = typeof(TEntity).Name.ToLower();
        var pluralizedName = _pluralizationProvider.Pluralize(lowercaseEntityName);
        var fileName = Path.ChangeExtension(pluralizedName, ".json");
        return Path.Combine(folderPath, fileName);
    }

    public async IAsyncEnumerable<TEntity> ReadAsync()
    {
        var directory = Directory.GetParent(_filePath)!;
        if(!directory.Exists || !File.Exists(_filePath)) yield break;

        var fileStream = File.OpenRead(_filePath);
        using var reader = new StreamReader(fileStream);
        await using var jsonReader = new JsonTextReader(reader);
        var dataArray = await JArray.LoadAsync(jsonReader);

        foreach (var dataObject in dataArray)
            if (IsValidEntry(dataObject, out var entity))
                yield return entity!;
    }

    public async ValueTask WriteAsync(IEnumerable<TEntity> entities)
    {
        var directory = Directory.GetParent(_filePath)!;
        if (!directory.Exists) directory.Create();

        await using var writer = new StreamWriter(_filePath);
        await using var jsonWriter = new JsonTextWriter(writer);
        await jsonWriter.WriteStartArrayAsync();

        foreach (var entity in entities)
            _serializer.Serialize(jsonWriter, entity);

        await jsonWriter.WriteEndArrayAsync();
        await jsonWriter.FlushAsync();
        await writer.FlushAsync();
    }

    public bool IsValidEntry(JToken token, out TEntity? entity)
    {
        entity = null;
        var dataString = token.ToString();

        return !string.IsNullOrWhiteSpace(dataString) &&
               (entity = JsonConvert.DeserializeObject<TEntity>(dataString)) != null;
    }
}