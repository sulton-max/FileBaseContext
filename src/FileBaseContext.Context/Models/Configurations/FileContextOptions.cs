using FileBaseContext.Abstractions.Models.FileContext;

namespace FileBaseContext.Context.Models.Configurations;

public interface IFileContextOptions<out TFileContext> where TFileContext : IFileContext
{
    string StorageRootPath { get; }
    bool FetchOnContextCreation { get; }
}

public record FileContextOptions<TFileContext>(string StorageRootPath, bool FetchOnContextCreation = false) : IFileContextOptions<TFileContext>
    where TFileContext : IFileContext;