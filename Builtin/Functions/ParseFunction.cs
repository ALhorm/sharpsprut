using Sprut.Lib;

namespace Sprut.Builtin.Functions;

internal class ParseFunction : Function
{
    private string type;

    public ParseFunction(string type)
    {
        this.type = type;
        isConst = true;
        Args = new Dictionary<string, IValue>()
        {
            ["x"] = new VoidValue()
        };
    }

    public override IValue Exec()
    {
        return type switch
        {
            "number" => new NumberValue(Args["x"].AsNumber()),
            "string" => new StringValue(Args["x"].AsString()),
            "bool" => new BoolValue(Args["x"].AsBool()),
            _ => throw new Exception("unexpected type conversion.")
        };
    }
}
