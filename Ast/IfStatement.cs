namespace Sprut.Ast;

internal struct IfStatement : IStatement
{
    private List<IExpression> conditions;
    private List<IStatement> statements;
    private IStatement? elseStatement;

    public IfStatement(List<IExpression> conditions, List<IStatement> statements, IStatement? elseStatement)
    {
        this.conditions = conditions;
        this.statements = statements;
        this.elseStatement = elseStatement;
    }

    public void Exec()
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].Eval().AsBool())
            {
                statements[i].Exec();
                return;
            }
        }
        elseStatement?.Exec();
    }
}
