using Sprut.Lib;

namespace Sprut.Ast;

internal struct BinaryExpression : IExpression
{
    private IExpression expr1, expr2;
    private char operation;

    public BinaryExpression(IExpression expr1, IExpression expr2, char operation)
    {
        this.expr1 = expr1;
        this.expr2 = expr2;
        this.operation = operation;
    }

    public IValue Eval()
    {
        var value1 = expr1.Eval();
        var value2 = expr2.Eval();

        if (operation == '^') return new NumberValue((float)Math.Pow(value1.AsNumber(), value2.AsNumber()));

        return operation switch
        {
            '+' => value1 + value2,
            '-' => value1 - value2,
            '*' => value1 * value2,
            '/' => value1 / value2,
            _ => throw new Exception("incorrect operation.")
        };
    }
}
