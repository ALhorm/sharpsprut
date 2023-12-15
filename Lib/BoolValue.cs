namespace Sprut.Lib;

internal struct BoolValue : IValue
{
    private bool value;

    public BoolValue(bool value)
    {
        this.value = value;
    }

    public bool AsBool() => value;

    public float AsNumber() => value ? 1f : 0f;

    public string AsString() => value.ToString();

    public override string? ToString() => value.ToString().ToLower();
}
