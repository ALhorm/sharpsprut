using Sprut.Lib;

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
        var function = expression.SetFunctionArgs(Functions.Get(expression.name));
        function.Exec();
    }
}
