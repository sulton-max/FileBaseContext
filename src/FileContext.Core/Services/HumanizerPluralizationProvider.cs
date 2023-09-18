using Humanizer;

namespace FileBaseContext.Set.Services;

public class HumanizerPluralizationProvider : IPluralizationProvider
{
    public string Pluralize(string word) => word.Pluralize();
}