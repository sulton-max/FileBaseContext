using FileBaseContext.Abstractions.Models.Entity;

namespace FileBaseContext.Samples.Context.Console.Models;

public class BlogPost : IFileSetEntity<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid AuthorId { get; set; }

    public BlogPost(Guid id, string title, string content, DateTime createdAt, Guid authorId)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        AuthorId = authorId;
    }
}