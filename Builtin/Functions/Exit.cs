using Sprut.Lib;

namespace Sprut.Builtin.Functions;

internal class ExitFunction : Function
{
    public ExitFunction()
    {
        IsConst = true;
        Args = new Dictionary<string, IValue>();
    }

    public override IValue Exec()
    {
        Environment.Exit(0);
        return new VoidValue();
    }
}
