using Humanizer;

namespace FileContext.Core.Services;

public class HumanizerPluralizationProvider : IPluralizationProvider
{
    public string Pluralize(string word) => word.Pluralize();
}