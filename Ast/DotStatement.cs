namespace Sprut.Ast;

internal struct DotStatement : IStatement
{
    private DotExpression expression;

    public DotStatement(DotExpression expression)
    {
        this.expression = expression;
    }

    public void Exec() => expression.Eval();
}
