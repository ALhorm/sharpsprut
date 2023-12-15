using Sprut.Lib;

namespace Sprut.Ast;

internal struct ValueExpression : IExpression
{
    private IValue value;

    public ValueExpression(float value) => this.value = new NumberValue(value);
    public ValueExpression(string value) => this.value = new StringValue(value);
    public ValueExpression(bool value) => this.value = new BoolValue(value);

    public IValue Eval() => value;
}
