using Data.SO;
using Infrastructure.Bootstrap;

namespace Core
{
    public interface ISettingsProvider : IBootable
    {
        T GetSettings<T>() where T : SettingsBase;
    }
}
