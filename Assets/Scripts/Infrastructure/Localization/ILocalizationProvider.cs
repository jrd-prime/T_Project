using Infrastructure.Bootstrap;

namespace Infrastructure.Localization
{
    public interface ILocalizationProvider : IBootable
    {
        string Localize(string key, WordTransform wordTransform = WordTransform.None);
    }
}
