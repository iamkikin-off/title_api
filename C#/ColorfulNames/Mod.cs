using ColorfulNames;
using GDWeave;

namespace ColorfulNames;

public class Mod : IMod {
    
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.RegisterScriptMod(new Main());
        modInterface.Logger.Information("Loaded!");
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
