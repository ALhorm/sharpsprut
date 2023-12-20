using Sprut.Lib;

namespace Sprut.Ast;

internal struct FunctionStatement : IStatement
{
    private string name;
    private Dictionary<string, IExpression?> args;
    private bool isConst;
    private IStatement statement;

    public FunctionStatement(string name, Dictionary<string, IExpression?> args, IStatement statement, bool isConst)
    {
        this.name = name;
        this.args = args;
        this.isConst = isConst;
        this.statement = statement;
    }

    public void Exec()
    {
        var function = new Function(statement, isConst);
        foreach (var arg in args) function.Args.Add(arg.Key, arg.Value != null ? arg.Value.Eval() : new VoidValue());
        Functions.Set(name, function);
    }
}
