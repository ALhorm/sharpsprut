namespace Sprut;

internal class Lexer
{
    private string code;
    private int pos = 0;
    private List<Token> tokens = new List<Token>();
    private Dictionary<string, TokenType> operators = new Dictionary<string, TokenType>()
    {
        ["+"] = TokenType.PLUS,
        ["-"] = TokenType.MINUS,
        ["*"] = TokenType.STAR,
        ["/"] = TokenType.SLASH,
        ["("] = TokenType.OPEN_RND_BKT,
        [")"] = TokenType.CLOSE_RND_BKT,
        ["{"] = TokenType.OPEN_CRL_BKT,
        ["}"] = TokenType.CLOSE_CRL_BKT,
        ["["] = TokenType.OPEN_SQR_BKT,
        ["]"] = TokenType.CLOSE_SQR_BKT,
        ["="] = TokenType.ASSIGN,
        ["^"] = TokenType.POWER,
        [">"] = TokenType.GREATER,
        ["<"] = TokenType.LESS,
        [","] = TokenType.COMMA,
        ["=="] = TokenType.EQUAL,
        [">="] = TokenType.GREATER_EQUAL,
        ["<="] = TokenType.LESS_EQUAL,
        ["+="] = TokenType.PLUS_ASSIGN,
        ["-="] = TokenType.MINUS_ASSIGN,
        ["*="] = TokenType.STAR_ASSIGN,
        ["/="] = TokenType.SLASH_ASSIGN
    };
    private string chars = "+-*/(){}[]=<>^,";

    public Lexer(string code) => this.code = code;

    public List<Token> Tokenize()
    {
        while (pos < code.Length)
        {
            var current = Peek(0);

            if (char.IsDigit(current)) TokenizeNumber();
            else if (chars.Contains(current)) TokenizeOperator();
            else if (char.IsLetter(current)) TokenizeWord();
            else if (current == '"') TokenizeText();
            else Next();
        }

        return tokens;
    }

    private void TokenizeNumber()
    {
        var current = Peek(0);
        var result = "";

        while (char.IsDigit(current) || current == '.')
        {
            if (result.Contains('.') && current == '.') throw new Exception("incorrect float number.");
            result += current;
            current = Next();
        }

        AddToken(TokenType.NUMBER, result);
    }

    private void TokenizeOperator()
    {
        var current = Peek(0);
        var result = "";

        while (true)
        {
            if (!operators.ContainsKey(result + current) && !string.IsNullOrEmpty(result))
            {
                AddToken(operators[result], result);
                return;
            }

            result += current;
            current = Next();
        }
    }

    private void TokenizeWord()
    {
        var current = Peek(0);
        var result = "";

        while (true)
        {
            if (!char.IsLetterOrDigit(current) && current != '_' && current != '$') break;
            result += current;
            current = Next();
        }

        switch (result)
        {
            case "true": AddToken(TokenType.TRUE, result); break;
            case "false": AddToken(TokenType.FALSE, result); break;
            case "if": AddToken(TokenType.IF, result); break;
            case "else": AddToken(TokenType.ELSE, result); break;
            case "elif": AddToken(TokenType.ELIF, result); break;
            case "while": AddToken(TokenType.WHILE, result); break;
            case "log": AddToken(TokenType.LOG, result); break;
            case "for": AddToken(TokenType.FOR, result); break;
            case "void": AddToken(TokenType.VOID, result); break;
            case "fun": AddToken(TokenType.FUN, result); break;
            case "return": AddToken(TokenType.RETURN, result); break;
            default: AddToken(TokenType.WORD, result); break;
        }
    }

    private void TokenizeText()
    {
        Next();
        var current = Peek(0);
        var result = "";

        while (true)
        {
            if (current == '"') break;
            result += current;
            current = Next();
        }
        Next();

        AddToken(TokenType.TEXT, result);
    }

    private void AddToken(TokenType type, string value) => tokens.Add(new Token(type, value));

    private char Peek(int relativePosition)
    {
        var position = pos + relativePosition;
        if (position >= code.Length) return '\0';
        return code[position];
    }

    private char Next()
    {
        pos++;
        return Peek(0);
    }
}
