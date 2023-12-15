namespace Sprut.Ast;

internal struct LogStatement : IStatement
{
    private IExpression expression;

    public LogStatement(IExpression expression)
    {
        this.expression = expression;
    }

    public void Exec() => Console.Write(expression.Eval().AsString());
}
