using Sprut.Lib;

namespace Sprut.Ast;

internal struct UnaryExpression : IExpression
{
    private IExpression expr;
    private char operation;

    public UnaryExpression(IExpression expr, char operation)
    {
        this.expr = expr;
        this.operation = operation;
    }

    public IValue Eval()
    {
        return operation switch
        {
            '+' => new NumberValue(expr.Eval().AsNumber()),
            '-' => new NumberValue(-expr.Eval().AsNumber()),
            _ => throw new Exception("incorrect operation.")
        };
    }
}
