using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Context.Models.FileContext;
using FileBaseContext.Samples.Context.Console.Models;

namespace FileBaseContext.Samples.Context.Console.DataAccess;

public class AppDataContext : FileContext
{
    public static string StorageFolder { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Storage");

    public IFileSet<User, Guid> Users => Set<User>(nameof(Users));
    public IFileSet<BlogPost, Guid> Posts => Set<BlogPost>(nameof(Posts));

    public AppDataContext(FileContextOptions<AppDataContext> fileContextOptions) : base(fileContextOptions)
    {
    }
}