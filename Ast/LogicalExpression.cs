using Sprut.Lib;

namespace Sprut.Ast;

internal struct LogicalExpression : IExpression
{
    private IExpression expr1, expr2;
    private string operation;

    public LogicalExpression(IExpression expr1, IExpression expr2, string operation)
    {
        this.expr1 = expr1;
        this.expr2 = expr2;
        this.operation = operation;
    }

    public IValue Eval()
    {
        var value1 = expr1.Eval();
        var value2 = expr2.Eval();

        return operation switch
        {
            ">" => value1 > value2,
            "<" => value1 < value2,
            _ => throw new Exception("incorrect operation.")
        };
    }
}
