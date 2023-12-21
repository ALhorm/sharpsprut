using Sprut.Lib;

namespace Sprut.Ast;

internal struct StructStatement : IStatement
{
    private string name;
    private bool isConst;
    private BlockStatement block;

    public StructStatement(string name, bool isConst, BlockStatement block)
    {
        this.name = name;
        this.isConst = isConst;
        this.block = block;
    }

    public void Exec()
    {
        var structure = new Structure(name, isConst);

        foreach (var statement in block.statements)
        {
            if (statement is FunctionStatement)
            {
                var function = (FunctionStatement)statement;
                structure.SetFunction(function.name, function.GetFunction());
            }
            else if (statement is VariableStatement)
            {
                var variable = (VariableStatement)statement;
                structure.SetVariable(variable.name, variable.GetVariable());
            }
            else throw new Exception("incorrect structure.");
        }

        Structures.Set(name, structure);
        Parser.structure = null;
    }
}
