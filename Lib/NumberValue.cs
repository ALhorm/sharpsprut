﻿namespace Sprut.Lib;

internal struct NumberValue : IValue
{
    private float value;

    public NumberValue(float value)
    {
        this.value = value;
    }

    public bool AsBool() => value != 0f;

    public List<IValue> AsList() => throw new Exception("cannot convert number to list.");

    public float AsNumber() => value;

    public Structure AsStruct() => throw new Exception("cannot convert number to structure.");

    public string AsString() => value.ToString();

    public override string? ToString() => value.ToString();

    public string TypeName() => "number";
}
