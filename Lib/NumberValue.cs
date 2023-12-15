namespace Sprut.Lib;

internal struct NumberValue : IValue
{
    private float value;

    public NumberValue(float value)
    {
        this.value = value;
    }

    public bool AsBool() => value != 0f;

    public float AsNumber() => value;

    public string AsString() => value.ToString();

    public override string? ToString() => value.ToString();
}
