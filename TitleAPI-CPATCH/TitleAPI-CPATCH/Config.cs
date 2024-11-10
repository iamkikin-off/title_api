using System.Text.Json.Serialization;

namespace TitleAPI_CPATCH;

public class Config {
    [JsonInclude] public bool SomeSetting = true;
}
