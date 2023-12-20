namespace Sprut.Lib;

internal struct Variable
{
    public IValue Value { get; init; }
    public int HashCode { get; init; }
    public bool IsConst { get; init; }

    public Variable(IValue value, bool isConst)
    {
        var random = new Random();

        Value = value;
        HashCode = random.Next((int)Math.Pow(10, 10), (int)Math.Pow(10, 6));
        IsConst = isConst;
    }
}
