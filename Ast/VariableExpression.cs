using Sprut.Lib;

namespace Sprut.Ast;

internal struct VariableExpression : IExpression
{
    private string name;

    public VariableExpression(string name)
    {
        this.name = name;
    }

    public IValue Eval()
    {
        if (Variables.IsExists(name)) return Variables.Get(name);
        throw new Exception($"variable \"{name}\" doesn't exist.");
    }
}
