namespace FileBaseContext.Abstractions.Models.Entity;

public interface IFileSetEntity<TKey> where TKey : struct
{
    TKey Id { get; set; }
}