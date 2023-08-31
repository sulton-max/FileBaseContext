using FileContext.Abstractions.Models.FileSet;
using FileContext.Core.Models.FileSet;
using FileContext.Samples.FileSet.Console.Models;

namespace FileContext.Samples.FileSet.Console.DataAccess;

public class AppDataContext
{
    public IFileSet<User, Guid> Users = new FileSet<User, Guid>(Directory.GetCurrentDirectory(), null, null);
    public IFileSet<BlogPost, Guid> Posts = new FileSet<BlogPost, Guid>(Directory.GetCurrentDirectory(), null, null);

    public AppDataContext()
    {
        Task.WaitAll(Users.FetchAsync().AsTask(), Posts.FetchAsync().AsTask());
    }

    public async ValueTask SaveChangesAsync()
    {
        await Users.SaveChangesAsync();
        await Posts.SaveChangesAsync();
    }
}