namespace Sprut.Ast;

internal struct WhileStatement : IStatement
{
    private IExpression expression;
    private IStatement statement;

    public WhileStatement(IExpression expression, IStatement statement)
    {
        this.expression = expression;
        this.statement = statement;
    }

    public void Exec()
    {
        while (expression.Eval().AsBool()) statement.Exec();
    }
}
