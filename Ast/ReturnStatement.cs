namespace Sprut.Ast;

internal class ReturnException : Exception
{
    public IExpression expression { get; init; }

    public ReturnException(IExpression expression)
    {
        this.expression = expression;
    }
}

internal struct ReturnStatement : IStatement
{
    private IExpression expr;

    public ReturnStatement(IExpression expr)
    {
        this.expr = expr;
    }

    public void Exec() => throw new ReturnException(expr);
}
