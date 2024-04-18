using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace KVSK_X;

public class PluginConfig : BasePluginConfig
{
    [JsonPropertyName("SetTag")] public string SetTag { get; set; } = "[KvsK]";
    [JsonPropertyName("HudTeam")] public int HudTeam { get; set; } = 1;
    [JsonPropertyName("SetHP")] public int SetHP { get; set; } = 35; 
}