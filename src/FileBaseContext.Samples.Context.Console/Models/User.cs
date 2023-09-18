using FileBaseContext.Abstractions.Models.Entity;

namespace FileBaseContext.Samples.Context.Console.Models;

public class User : IFileSetEntity<Guid>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public User(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}