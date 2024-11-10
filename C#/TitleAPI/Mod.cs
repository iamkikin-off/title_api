using GDWeave;

namespace TitleAPI;

public class Mod : IMod {

    public Mod(IModInterface modInterface) {
        modInterface.RegisterScriptMod(new PlayerLabelPatch());
    }

    public void Dispose() {
        
    }
}
