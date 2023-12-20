using Sprut.Lib;

namespace Sprut.Ast;

internal struct FunctionCallExpression : IExpression
{
    private string name;
    private Dictionary<Dictionary<int, string?>, IExpression> args;

    public FunctionCallExpression(string name, Dictionary<Dictionary<int, string?>, IExpression> args)
    {
        this.name = name;
        this.args = args;
    }

    public Function GetFunction()
    {
        var function = Functions.Get(name);

        foreach (var arg in args)
        {
            var value = arg.Value.Eval();

            foreach (var key in arg.Key)
            {
                if (key.Value != null) function.Args[key.Value] = value;
                else function.Args[function.Args.ElementAt(key.Key).Key] = value;
            }
        }

        return function;
    }

    public IValue Eval()
    {
        var function = GetFunction();
        return function.Exec();
    }
}
