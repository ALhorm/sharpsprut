namespace Sprut.Lib;

internal interface IValue
{
    float AsNumber();
    string AsString();
    bool AsBool();
    List<IValue> AsList();
    Structure AsStruct();
    string TypeName();

    public static IValue operator +(IValue val1, IValue val2)
    {
        if (val1 is NumberValue && val2 is NumberValue) return new NumberValue(val1.AsNumber() + val2.AsNumber());
        if (val1 is StringValue && val2 is StringValue) return new StringValue(val1.AsString() + val2.AsString());
        throw new Exception("unexpected type.");
    }

    public static IValue operator -(IValue val1, IValue val2) => new NumberValue(val1.AsNumber() - val2.AsNumber());

    public static IValue operator *(IValue val1, IValue val2) => new NumberValue(val1.AsNumber() * val2.AsNumber());

    public static IValue operator /(IValue val1, IValue val2) => new NumberValue(val1.AsNumber() / val2.AsNumber());

    public static IValue operator >(IValue val1, IValue val2)
    {
        if (val1 is NumberValue && val2 is NumberValue) return new BoolValue(val1.AsNumber() > val2.AsNumber());
        if (val1 is StringValue && val2 is StringValue) return new BoolValue(val1.AsString().Length > val2.AsString().Length);
        throw new Exception("unexpected type.");
    }

    public static IValue operator <(IValue val1, IValue val2)
    {
        if (val1 is NumberValue && val2 is NumberValue) return new BoolValue(val1.AsNumber() < val2.AsNumber());
        if (val1 is StringValue && val2 is StringValue) return new BoolValue(val1.AsString().Length < val2.AsString().Length);
        throw new Exception("unexpected type.");
    }
}
