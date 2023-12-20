using Sprut.Ast;

namespace Sprut.Lib;

internal class Function
{
    public Dictionary<string, IValue> Args = new Dictionary<string, IValue>();
    public bool isConst { get; init; }
    private IStatement? statement;

    public Function() { }

    public Function(IStatement statement, bool isConst)
    {
        this.isConst = isConst;
        this.statement = statement;
    }

    public virtual IValue Exec()
    {
        Variables.Set(Args);
        
        try
        {
            statement?.Exec();
        }
        catch (ReturnException re)
        {
            Variables.Pop(Args);
            return re.expression.Eval();
        }

        Variables.Pop(Args);
        return new VoidValue();
    }
}
