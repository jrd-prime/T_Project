using Data.SO;
using Infrastructure.Bootstrap;

namespace Infrastructure.Settings
{
    public interface ISettingsProvider : IBootable
    {
        T GetSettings<T>() where T : SettingsBase;
    }
}
