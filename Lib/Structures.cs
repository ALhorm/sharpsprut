using Sprut.Builtin.Structures;

namespace Sprut.Lib;

internal struct Structures
{
    private static Dictionary<string, Structure> structures = new Dictionary<string, Structure>()
    {
        ["Console"] = new ConsoleStructure()
    };

    public static Structure Get(string name) => structures[name];

    public static bool IsExists(string name) => structures.ContainsKey(name);

    public static void Set(string name, Structure structure) => structures[name] = structure;
}
