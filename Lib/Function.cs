using Sprut.Ast;

namespace Sprut.Lib;

internal class Function
{
    public Dictionary<string, IValue> Args = new Dictionary<string, IValue>();
    public bool IsConst { get; init; }
    public bool IsPublic { get; init; } = false;
    private IStatement? statement;

    public Function() { }

    public Function(IStatement? statement, bool isConst, bool isPublic)
    {
        IsConst = isConst;
        IsPublic = isPublic;
        this.statement = statement;
    }

    public virtual IValue Exec()
    {
        Variables.Set(Args);
        IValue result = new VoidValue();
        
        try
        {
            statement?.Exec();
        }
        catch (ReturnException re)
        {
            result = re.expression.Eval();
        }

        Variables.Pop(Args);
        return result;
    }
}
