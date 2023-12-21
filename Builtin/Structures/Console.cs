using Sprut.Lib;

namespace Sprut.Builtin.Structures;

internal class PrintFunction : Function
{
    public PrintFunction()
    {
        IsConst = true;
        IsPublic = true;
        Args = new Dictionary<string, IValue>()
        {
            ["x"] = new VoidValue()
        };
    }

    public override IValue Exec()
    {
        Console.Write(Args["x"].AsString());
        return new VoidValue();
    }
}

internal class InputFunction : Function
{
    public InputFunction()
    {
        IsConst = true;
        IsPublic = true;
        Args = new Dictionary<string, IValue>()
        {
            ["x"] = new StringValue("")
        };
    }

    public override IValue Exec()
    {
        Console.Write(Args["x"].AsString());
        var result = Console.ReadLine();
        if (result != null) return new StringValue(result);
        return new VoidValue();
    }
}

internal class ConsoleStructure : Structure
{
    public ConsoleStructure()
    {
        Name = "Console";
        IsConst = true;
        functions = new Dictionary<string, Function>()
        {
            ["print"] = new PrintFunction(),
            ["input"] = new InputFunction()
        };
    }
}
