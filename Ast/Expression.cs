using Sprut.Lib;

namespace Sprut.Ast;

internal interface IExpression
{
    IValue Eval();
}
