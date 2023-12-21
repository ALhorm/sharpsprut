namespace Sprut.Lib;

internal struct Variables
{
    private static Dictionary<string, Variable> variables = new Dictionary<string, Variable>()
    {
        ["console"] = new Variable(new StructValue(Structures.Get("Console"), "console"), true)
    };

    public static Variable Get(string name) => variables[name];

    public static bool IsExists(string name) => variables.ContainsKey(name);

    public static void Set(string name, Variable value) => variables[name] = value;

    public static void Set(Dictionary<string, IValue> vars)
    {
        foreach (var variable in vars) variables.Add(variable.Key, new Variable(variable.Value, false));
    }

    public static void Set(Dictionary<string, Variable> vars)
    {
        foreach (var variable in vars) variables.Add(variable.Key, variable.Value);
    }

    public static void Pop(string name) => variables.Remove(name);

    public static void Pop(Dictionary<string, IValue> vars)
    {
        foreach (var variable in vars) variables.Remove(variable.Key);
    }

    public static void Pop(Dictionary<string, Variable> vars)
    {
        foreach (var variable in vars) variables.Remove(variable.Key);
    }
}
