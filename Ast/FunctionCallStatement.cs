namespace Sprut.Ast;

internal struct FunctionCallStatement : IStatement
{
    private FunctionCallExpression expression;

    public FunctionCallStatement(FunctionCallExpression expression)
    {
        this.expression = expression;
    }

    public void Exec()
    {
        var function = expression.GetFunction();
        function.Exec();
    }
}
