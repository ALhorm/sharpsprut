using Sprut.Lib;

namespace Sprut.Builtin.Functions;

internal class TypeOfFunction : Function
{
    public TypeOfFunction()
    {
        IsConst = true;
        Args = new Dictionary<string, IValue>()
        {
            ["x"] = new VoidValue()
        };
    }

    public override IValue Exec() => new StringValue(Args["x"].TypeName());
}
