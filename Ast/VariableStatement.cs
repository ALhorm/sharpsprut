using Sprut.Lib;

namespace Sprut.Ast;

internal struct VariableStatement : IStatement
{
    private string name;
    private IExpression expr;

    public VariableStatement(string name, IExpression expr)
    {
        this.name = name;
        this.expr = expr;
    }

    public void Exec() => Variables.Set(name, expr.Eval());
}
