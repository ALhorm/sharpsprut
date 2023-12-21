using Sprut.Lib;

namespace Sprut.Ast;

internal struct VariableStatement : IStatement
{
    public string name { get; init; }
    private string operation;
    private string? structure;
    private bool isConst, isStruct;
    private IExpression expr;

    public VariableStatement(string name, string operation, string? structure, bool isConst, bool isStruct, IExpression expr)
    {
        this.name = name;
        this.operation = operation;
        this.structure = structure;
        this.isConst = isConst;
        this.isStruct = isStruct;
        this.expr = expr;
    }

    public Variable GetVariable() => new Variable(expr.Eval(), isConst);

    public void Exec()
    {
        if (!isStruct)
        {
            if (Variables.IsExists(name) && Variables.Get(name).IsConst)
                throw new Exception($"constant \"{name}\" cannot be overridden.");
            if (operation == "=") Variables.Set(name, new Variable(expr.Eval(), isConst));
            var variable = Variables.Get(name);

            switch (operation)
            {
                case "+=": Variables.Set(name, new Variable(expr.Eval() + variable.Value, isConst)); break;
                case "-=": Variables.Set(name, new Variable(expr.Eval() - variable.Value, isConst)); break;
                case "*=": Variables.Set(name, new Variable(expr.Eval() * variable.Value, isConst)); break;
                case "/=": Variables.Set(name, new Variable(expr.Eval() / variable.Value, isConst)); break;
            }
            return;
        }

        if (structure != null) Structures.Get(structure).SetVariable(name, new Variable(expr.Eval(), isConst));
    }
}
