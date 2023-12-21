namespace Sprut.Lib;

internal struct VoidValue : IValue
{
    public bool AsBool() => false;

    public List<IValue> AsList() => throw new Exception("cannot convert void to list.");

    public float AsNumber() => 0f;

    public Structure AsStruct() => throw new Exception("cannot convert void to structure.");

    public string AsString() => "void";

    public string TypeName() => "void";
}
