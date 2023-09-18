using Humanizer;

namespace FileBaseContext.FileSet.Services;

public class HumanizerPluralizationProvider : IPluralizationProvider
{
    public string Pluralize(string word) => word.Pluralize();
}