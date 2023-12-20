namespace Sprut.Ast;

internal struct ForStatement : IStatement
{
    private IStatement init, action, statement;
    private IExpression condition;

    public ForStatement(IStatement init, IExpression condition, IStatement action, IStatement statement)
    {
        this.init = init;
        this.action = action;
        this.statement = statement;
        this.condition = condition;
    }

    public void Exec()
    {
        for (init.Exec(); condition.Eval().AsBool(); action.Exec()) statement.Exec();
    }
}
