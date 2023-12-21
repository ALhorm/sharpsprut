namespace Sprut.Lib;

internal struct StructValue : IValue
{
    private Structure value;
    public string instance { get; init; }

    public StructValue(Structure value, string instance)
    {
        this.value = value;
        this.instance = instance;
    }

    public bool AsBool() => throw new Exception("cannot convert struct to bool.");

    public List<IValue> AsList() => throw new Exception("cannot convert struct to list.");

    public float AsNumber() => throw new Exception("cannot convert struct to number.");

    public Structure AsStruct() => value;

    public string AsString() => throw new Exception("cannot convert struct to string.");

    public string TypeName() => "structure";
}
