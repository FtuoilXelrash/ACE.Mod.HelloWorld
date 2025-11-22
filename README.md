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

## Configuring Build and Dependency Paths

The project is configured for a specific directory structure. If your paths differ, you'll need to update the `.csproj` file.

### Current Configuration

The `.csproj` file contains these key paths:

```xml
<!-- Where ACE DLL dependencies are located -->
<ACEPath>D:\DEV\ACE SERVER\ACE-CONTRIB\ACE-master\Source\ACE.Server\bin\x64\Debug\net8.0</ACEPath>

<!-- Where compiled DLLs are output -->
<OutputPath>D:\DEV\ACE SERVER\ACHARD-TEST\MODS\$(AssemblyName)</OutputPath>
```

### Changing the Dependency Path (ACE DLLs)

If your ACE DLLs are in a different location:

1. Open `HelloWorld.csproj` in a text editor
2. Find the `<PropertyGroup>` section with `<ACEPath>`
3. Update the path to where your ACE assemblies are located:

```xml
<PropertyGroup Condition="!$(Realms)">
    <ACEPath>D:\YOUR\PATH\TO\ACE\DLL\FOLDER</ACEPath>
</PropertyGroup>
```

**Common locations:**
- Debug build: `D:\DEV\ACE SERVER\ACE-CONTRIB\ACE-master\Source\ACE.Server\bin\x64\Debug\net8.0`
- Release build: `D:\DEV\ACE SERVER\ACE-CONTRIB\ACE-master\Source\ACE.Server\bin\x64\Release\net8.0`

### Changing the Build Output Path

If you want compiled DLLs to go to a different folder:

1. Open `HelloWorld.csproj` in a text editor
2. Find the `<PropertyGroup>` section with `<OutputPath>`
3. Change it to your desired output location:

```xml
<OutputPath>D:\YOUR\MOD\OUTPUT\PATH\$(AssemblyName)</OutputPath>
```

The `$(AssemblyName)` variable will be replaced with your project's assembly name (usually the `.csproj` filename without extension).

**For example:**
- `D:\TEMP\Mods\$(AssemblyName)` → outputs to `D:\TEMP\Mods\HelloWorld\`
- `C:\MyMods\$(AssemblyName)` → outputs to `C:\MyMods\HelloWorld\`

### For ACRealms Projects

If you're building for ACRealms instead of ACEmulator, the paths are set separately:

1. Uncomment this line in `.csproj`:
```xml
<DefineConstants>$(DefineConstants);REALM</DefineConstants>
```

2. Update the Realms-specific ACE path:
```xml
<PropertyGroup Condition="$(Realms)">
    <ACEPath>D:\YOUR\PATH\TO\REALMSERVER\DLL\FOLDER</ACEPath>
</PropertyGroup>
```

### Verifying Paths Are Correct

After updating paths, verify they work:

1. Run `dotnet restore HelloWorld.sln` - this will download NuGet dependencies
2. Check that the ACE DLL folder actually exists and contains `.dll` files
3. Run `dotnet build HelloWorld.sln` - if it fails, check the error messages for path issues
4. Verify the output folder was created and contains `HelloWorld.dll`

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
