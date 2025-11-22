namespace HelloWorld;

public class Mod : BasicMod
{
    public Mod() : base() => Setup(nameof(HelloWorld), new PatchClass(this));
}
