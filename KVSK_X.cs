using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Config;
using System.Drawing;

namespace KVSK_X;
public class KVSK_X : BasePlugin, IPluginConfig<PluginConfig>
{
    /*author*/
    public override string ModuleAuthor => "XnN.Prod";
    public override string ModuleName => "35HP KVK X";
    public override string ModuleVersion => "v0.0.4";
    public override string ModuleDescription => "Plugin for 35HP Servers.";
    //
    public string PluginTag = "[KvsK]";
    public int ShowHUD = 1;
    public int SetHealt = 35;
    public PluginConfig Config { get; set; }
    //


    public void OnConfigParsed(PluginConfig config)
    {
        config = ConfigManager.Load<PluginConfig>("KVSK_X");

        PluginTag = config.SetTag;
        ShowHUD = config.HudTeam;
        SetHealt = config.SetHP;
        Config = config;
    } 

    public override void Load(bool hotReload)
    {
        Console.WriteLine($"Loading...");
        Console.WriteLine($" ");
        Console.WriteLine($"{ModuleName} [{ModuleVersion}] loaded!!  "); 

        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        RegisterEventHandler<EventPlayerDeath>(EventPlayerDeath);       
    }

    

    public HookResult EventPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        if(ShowHUD != 1)
        { 
            int ct = 0, t = 0;
            foreach (var player in Utilities.GetPlayers().Where(player => player is { IsValid: true, PawnIsAlive: true }))
            {
                if(player.TeamNum == 2)t++;
                if(player.TeamNum == 3)ct++;
                player.PrintToCenter($" T Players: {t} | CT Players: {ct}");   
            }
        }      
        return HookResult.Continue;
    }


    public HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || player.PlayerPawn == null || !player.IsValid || !player.PlayerPawn.IsValid|| player.Connected != PlayerConnectedState.PlayerConnected)
        {
            return HookResult.Continue;
        }
        if (player.PlayerPawn.Value.Health > SetHealt)
        {
            Server.NextFrame(() =>
            {
                player.PlayerPawn.Value.Health = SetHealt;
                Utilities.SetStateChanged(player.PlayerPawn.Value, "CBaseEntity", "m_iHealth");
            });
        }
        return HookResult.Continue;
    }
}