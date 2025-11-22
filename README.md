# HelloWorld - Player Login Greeting Mod

This mod sends a "Hello World!" message to players when they log in.

## Project Structure

- **HelloWorld.sln** - Solution file (open this in Visual Studio)
- **HelloWorld.csproj** - Project file with all build configuration
- **Mod.cs** - Main mod class entry point
- **PatchClass.cs** - Harmony patches and mod logic
- **GlobalUsings.cs** - Global using statements for ACE/Harmony
- **Meta.json** - Mod metadata
- **Settings.json** - Your mod settings (auto-generated at runtime)

## Building

```bash
# First time: restore NuGet packages
dotnet restore HelloWorld.sln

# Build
dotnet build HelloWorld.sln

# Build in Release mode (creates ZIP)
dotnet build HelloWorld.sln -c Release
```

Build output goes to: `D:\DEV\ACE SERVER\ACHARD-TEST\MODS\HelloWorld\`

## Renaming This Mod

To create a new mod, rename this project:

1. Update `Mod.cs` - change namespace and class name
2. Update `PatchClass.cs` - change namespace
3. Update `Meta.json` - change the "Name" field
4. Rename `HelloWorld.csproj` to your mod name
5. Rename `HelloWorld.sln` to your mod name
6. Update `.sln` to reference the new `.csproj` name
7. Update `OutputPath` in `.csproj` if needed

## Adding Your Own Code

1. Open `PatchClass.cs` and add your Harmony patches and commands
2. Update `Meta.json` with your mod details
3. Build and the DLL will be ready in `D:\DEV\ACE SERVER\ACHARD-TEST\MODS\{ModName}\`

## What This Mod Does

This mod intercepts the `Player.PlayerEnterWorld()` method and sends a "Hello World!" message to the player as a private message when they log in.

## How It Works

The mod uses Harmony to patch the `Player.PlayerEnterWorld()` method:

```csharp
[HarmonyPatch(typeof(Player), nameof(Player.PlayerEnterWorld), new Type[] { })]
[HarmonyPostfix]
public static void OnPlayerEnterWorld(Player __instance)
{
    __instance.SendMessage("Hello World!");
}
```

When a player logs in, they receive a PM saying "Hello World!".

## Extending This Mod

To customize the message or add more functionality:

1. Edit the `OnPlayerEnterWorld` method in `PatchClass.cs`
2. Modify the message string or add additional logic
3. Rebuild with `dotnet build HelloWorld.sln`

## References

- **Harmony Documentation**: https://harmony.pardeike.net/
- **ACE.Shared Helpers**: Check the ACE-master repo for extension methods

## Notes

- This project is completely standalone and independent from ACE.BaseMod
- It uses NuGet packages (ACEmulator.ACE.Shared, Lib.Harmony)
- Supports both ACEmulator and ACRealms via conditional compilation (see REALM constant)
