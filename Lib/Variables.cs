namespace Sprut.Lib;

internal struct Variables
{
    private static Dictionary<string, IValue> variables = new Dictionary<string, IValue>();

    public static IValue Get(string name) => variables[name];

    public static bool IsExists(string name) => variables.ContainsKey(name);

    public static void Set(string name, IValue value) => variables[name] = value;
}
