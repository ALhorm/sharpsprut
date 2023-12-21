namespace Sprut.Ast;

internal struct BlockStatement : IStatement
{
    public List<IStatement> statements { get; init; }

    public BlockStatement(List<IStatement> statements)
    {
        this.statements = statements;
    }

    public void Exec()
    {
        foreach (var statement in statements) statement.Exec();
    }
}
