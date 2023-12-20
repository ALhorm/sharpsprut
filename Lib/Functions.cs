using Sprut.Builtin.Functions;

namespace Sprut.Lib;

internal struct Functions
{
    private static Dictionary<string, Function> functions = new Dictionary<string, Function>()
    {
        ["parseNumber"] = new ParseFunction("number"),
        ["parseString"] = new ParseFunction("string"),
        ["parseBool"] = new ParseFunction("bool")
    };

    public static Function Get(string name) => functions[name];

    public static bool IsExists(string name) => functions.ContainsKey(name);

    public static void Set(string name, Function value) => functions[name] = value;
}
