namespace Sprut.Lib;

internal struct StringValue : IValue
{
    private string value;

    public StringValue(string value)
    {
        this.value = value;
    }

    public bool AsBool() => bool.Parse(value);

    public float AsNumber() => float.Parse(value);

    public string AsString() => value;

    public override string? ToString() => value;
}
