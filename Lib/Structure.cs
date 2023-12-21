namespace Sprut.Lib;

internal class Structure : ICloneable
{
    public string Name { get; init; }
    public bool IsConst { get; init; }
    public string instance = "";

    protected Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
    protected Dictionary<string, Function> functions = new Dictionary<string, Function>();

    public Structure(string name, bool isConst)
    {
        Name = name;
        IsConst = isConst;
    }

    public Structure() { }

    public Variable GetVariable(string name) => variables[name];
    public void SetVariable(string name, Variable variable) => variables[name] = variable;

    public Function GetFunction(string name) => functions[name];
    public void SetFunction(string name, Function function) => functions[name] = function;

    public Structure GetInstance() => StructInstances.Get(instance);

    public override string? ToString() => $"<structure.{Name}>";

    public object Clone()
    {
        var result = new Structure(Name, IsConst);

        foreach (var variable in variables) result.SetVariable(variable.Key, variable.Value);
        foreach (var function in functions) result.SetFunction(function.Key, function.Value);

        return result;
    }
}
