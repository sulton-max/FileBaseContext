namespace FileBaseContext.Abstractions.Models.FileSet;

public interface IFileSetBase
{
    /// <summary>
    /// Fetches data from the file system
    /// </summary>
    /// <returns></returns>
    ValueTask FetchAsync();

    /// <summary>
    /// Synchronizes data to the file system
    /// </summary>
    /// <returns></returns>
    ValueTask SyncAsync();
}