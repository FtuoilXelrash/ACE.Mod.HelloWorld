
namespace HelloWorld;

[HarmonyPatch]
public class PatchClass(BasicMod mod, string settingsName = "Settings.json") : BasicPatch<Settings>(mod, settingsName)
{
    [HarmonyPatch(typeof(Player), nameof(Player.PlayerEnterWorld), new Type[] { })]
    [HarmonyPostfix]
    public static void OnPlayerEnterWorld(Player __instance)
    {
        __instance.SendMessage("Hello World!");
    }

    public override Task OnStartSuccess()
    {
        ModManager.Log($"HelloWorld started successfully!");
        return Task.CompletedTask;
    }

    public override void Stop()
    {
        base.Stop();
        ModManager.Log($"HelloWorld stopped!");
    }
}

public class Settings
{
    // Add your mod settings here
    // They will be automatically loaded/saved to Settings.json
}
