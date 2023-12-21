using Sprut.Lib;

namespace Sprut.Ast;

internal struct FunctionStatement : IStatement
{
    public string name { get; init; }
    private Dictionary<string, IExpression?> args;
    private bool isConst, isPublic;
    private IStatement statement;

    public FunctionStatement(string name, Dictionary<string, IExpression?> args, bool isConst, bool isPublic, IStatement statement)
    {
        this.name = name;
        this.args = args;
        this.isConst = isConst;
        this.isPublic = isPublic;
        this.statement = statement;
    }

    public Function GetFunction()
    {
        var function = new Function(statement, isConst, isPublic);
        foreach (var arg in args) function.Args.Add(arg.Key, arg.Value != null ? arg.Value.Eval() : new VoidValue());
        return function;
    }

    public void Exec()
    {
        if (Functions.IsExists(name) && Functions.Get(name).IsConst)
            throw new Exception($"constant function \"{name}\" cannot be overridden.");
        Functions.Set(name, GetFunction());
    }
}
