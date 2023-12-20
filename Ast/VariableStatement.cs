using Sprut.Lib;

namespace Sprut.Ast;

internal struct VariableStatement : IStatement
{
    private string name, operation;
    private IExpression expr;

    public VariableStatement(string name, string operation, IExpression expr)
    {
        this.name = name;
        this.expr = expr;
        this.operation = operation;
    }

    public void Exec()
    {
        if (operation == "=") Variables.Set(name, new Variable(expr.Eval(), false));
        var variable = Variables.Get(name);

        switch (operation)
        {
            case "+=": Variables.Set(name, new Variable(expr.Eval() + variable.Value, false)); break;
            case "-=": Variables.Set(name, new Variable(expr.Eval() - variable.Value, false)); break;
            case "*=": Variables.Set(name, new Variable(expr.Eval() * variable.Value, false)); break;
            case "/=": Variables.Set(name, new Variable(expr.Eval() / variable.Value, false)); break;
        }
    }
}
