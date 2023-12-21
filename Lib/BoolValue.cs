namespace Sprut.Lib;

internal struct BoolValue : IValue
{
    private bool value;

    public BoolValue(bool value)
    {
        this.value = value;
    }

    public bool AsBool() => value;

    public List<IValue> AsList() => throw new Exception("cannot convert bool to list.");

    public float AsNumber() => value ? 1f : 0f;

    public string AsString() => value.ToString();

    public Structure AsStruct() => throw new Exception("cannot convert bool to structure.");

    public override string? ToString() => value.ToString().ToLower();

    public string TypeName() => "bool";
}
