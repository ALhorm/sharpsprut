using Sprut.Lib;

namespace Sprut.Ast;

internal struct ThisExpression : IExpression
{
    private string name;

    public ThisExpression(string name)
    {
        this.name = name;
    }

    public IValue Eval()
    {
        if (Parser.structure == null) throw new Exception("not in structure.");
        return Structures.Get(Parser.structure).GetInstance().GetVariable(name).Value;
    }
}
