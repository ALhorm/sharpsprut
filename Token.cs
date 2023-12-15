namespace Sprut;

internal class Token
{
    public TokenType Type { get; init; }
    public string Value { get; init; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string? ToString() => $"Token({Type}, \"{Value}\")";
}
