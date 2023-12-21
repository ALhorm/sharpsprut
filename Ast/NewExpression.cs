using Sprut.Lib;

namespace Sprut.Ast;

internal struct NewExpression : IExpression
{
    private FunctionCallExpression expression;
    private string? instance;

    public NewExpression(FunctionCallExpression expression, string? instance)
    {
        this.expression = expression;
        this.instance = instance;
    }

    public IValue Eval()
    {
        var structure = Structures.Get(expression.name);
        var initFunction = expression.SetFunctionArgs(structure.GetFunction("_init"));
        initFunction.Exec();

        if (instance == null) instance = "_";
        StructInstances.Set(instance, (Structure)structure.Clone());

        return new StructValue(structure, instance);
    }
}
