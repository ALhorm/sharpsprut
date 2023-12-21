namespace Sprut.Lib;

internal struct StructInstances
{
    private static Dictionary<string, Structure> instances = new Dictionary<string, Structure>()
    {
        ["console"] = Structures.Get("Console")
    };

    public static Structure Get(string name) => instances[name];

    public static void Set(string name, Structure structure) => instances[name] = structure;

    public static void Print()
    {
        foreach (var instance in instances) Console.WriteLine($"{instance.Key}: {instance.Value.GetVariable("name").Value.AsString()}");
    }
}
