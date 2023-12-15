namespace Sprut.Ast;

internal struct BlockStatement : IStatement
{
    private List<IStatement> statements;

    public BlockStatement(List<IStatement> statements)
    {
        this.statements = statements;
    }

    public void Exec()
    {
        foreach (var statement in statements) statement.Exec();
    }
}
