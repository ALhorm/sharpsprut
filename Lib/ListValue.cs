namespace Sprut.Lib;

internal struct ListValue : IValue
{
    private List<IValue> value;

    public ListValue(List<IValue> value)
    {
        this.value = value;
    }

    public bool AsBool() => value.Count > 0;

    public List<IValue> AsList() => value;

    public float AsNumber() => throw new Exception("cannot convert list to number.");

    public Structure AsStruct() => throw new Exception("cannot convert list to structure.");

    public string AsString() => $"[{string.Join(", ", value)}]";

    public string TypeName() => "list";
}
