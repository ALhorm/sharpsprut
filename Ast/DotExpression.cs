using Sprut.Lib;

namespace Sprut.Ast;

internal struct DotExpression : IExpression
{
    private IExpression expression;
    private FunctionCallExpression? function;
    private string? name;

    public DotExpression(IExpression expression, FunctionCallExpression? function, string? name)
    {
        this.expression = expression;
        this.function = function;
        this.name = name;
    }

    public IValue Eval()
    {
        StructValue value = (StructValue)expression.Eval();
        var structure = value.AsStruct();
        if (value.instance != null) structure.instance = value.instance;
        Parser.structure = structure.Name;

        if (function != null)
        {
            var expr = (FunctionCallExpression)function;
            var func = structure.GetInstance().GetFunction(expr.name);

            if (func.IsPublic)
            {
                var result = expr.SetFunctionArgs(func).Exec();
                Parser.structure = null;
                return result;
            }
            throw new Exception("cannot call not public function.");
        }

        throw new Exception("incorrect dot operation.");
    }
}
