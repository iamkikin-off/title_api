using GDWeave;
using TitleAPI_CPATCH;

namespace TitleAPI_CPATCH;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new Main());
        modInterface.Logger.Information("HOT DOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOGS!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
