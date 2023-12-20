using Sprut.Lib;

namespace Sprut.Ast;

internal struct ListExpression : IExpression
{
    private List<IExpression> expressions;

    public ListExpression(List<IExpression> expressions)
    {
        this.expressions = expressions;
    }

    public IValue Eval()
    {
        var result = new List<IValue>();
        foreach (var expr in expressions) result.Add(expr.Eval());
        return new ListValue(result);
    }
}
