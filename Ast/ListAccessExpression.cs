using Sprut.Lib;

namespace Sprut.Ast;

internal struct ListAccessExpression : IExpression
{
    private IExpression expression;
    private List<IExpression> indices;

    public ListAccessExpression(IExpression expression, List<IExpression> indices)
    {
        this.expression = expression;
        this.indices = indices;
    }

    public IValue Eval()
    {
        var list = expression.Eval().AsList();
        IValue value = new VoidValue();

        foreach (var index in indices)
        {
            value = list[(int)index.Eval().AsNumber()];
            if (value is ListValue) list = value.AsList();
            else break;
        }

        return value;
    }
}
