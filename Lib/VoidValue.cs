namespace Sprut.Lib;

internal struct VoidValue : IValue
{
    public bool AsBool() => false;

    public List<IValue> AsList() => throw new Exception("cannot convert void to list.");

    public float AsNumber() => 0f;

    public string AsString() => "void";
}
