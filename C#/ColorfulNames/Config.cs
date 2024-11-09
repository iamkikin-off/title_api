using System.Text.Json.Serialization;

namespace ColorfulNames;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
