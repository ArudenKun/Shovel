using Shovel.Core.Models.LunarCore;

namespace Shovel.Models;

internal sealed class GlobalConfig
{
    public AppConfig AppConfig { get; }
    public LunarCoreConfig LunarCoreConfig { get; }

    public GlobalConfig()
    {
        AppConfig = new AppConfig();
        LunarCoreConfig = new LunarCoreConfig();
    }

    public void Save()
    {
        AppConfig.Save();
        LunarCoreConfig.Save();
    }

    public void Load()
    {
        AppConfig.Load();
        LunarCoreConfig.Load();
    }
}
