using Sprut.Ast;
using System.Data;

namespace Sprut;

internal class Parser
{
    private List<Token> tokens;
    private int pos = 0;
    private Token EOF = new Token(TokenType.EOF, "\0");

    public Parser(List<Token> tokens) => this.tokens = tokens;

    public List<IStatement> Parse()
    {
        var result = new List<IStatement>();
        while (!Match(TokenType.EOF)) result.Add(Statement());
        return result;
    }

    private IStatement Statement()
    {
        if (Match(TokenType.IF)) return If();
        if (Match(TokenType.WHILE)) return While();
        if (Match(TokenType.LOG)) return new LogStatement(Expression());

        return Variable();
    }

    private IStatement Variable()
    {
        if (LookMatch(TokenType.ASSIGN, 1))
        {
            var name = Consume(TokenType.WORD).Value;
            Consume(TokenType.ASSIGN);
            return new VariableStatement(name, Expression());
        }

        throw new Exception("unknown statement.");
    }

    private IStatement Block()
    {
        var statements = new List<IStatement>();

        Consume(TokenType.OPEN_CRL_BKT);
        while (!Match(TokenType.CLOSE_CRL_BKT)) statements.Add(Statement());

        return new BlockStatement(statements);
    }

    private IStatement BlockOrStatement() => LookMatch(TokenType.OPEN_CRL_BKT, 0) ? Block() : Statement();

    private IStatement If()
    {
        var conditions = new List<IExpression>();
        var statements = new List<IStatement>();
        IStatement? elseStatement;

        conditions.Add(Expression());
        statements.Add(BlockOrStatement());

        while (Match(TokenType.ELIF))
        {
            conditions.Add(Expression());
            statements.Add(BlockOrStatement());
        }

        elseStatement = Match(TokenType.ELSE) ? BlockOrStatement() : null;

        return new IfStatement(conditions, statements, elseStatement);
    }

    private IStatement While()
    {
        var expression = Expression();
        return new WhileStatement(expression, BlockOrStatement());
    }

    private IExpression Expression() => Logical();

    private IExpression Logical()
    {
        var result = Additive();

        while (true)
        {
            if (Match(TokenType.GREATER))
            {
                result = new LogicalExpression(result, Additive(), ">");
                continue;
            }
            if (Match(TokenType.LESS))
            {
                result = new LogicalExpression(result, Additive(), "<");
                continue;
            }
            break;
        }

        return result;
    }

    private IExpression Additive()
    {
        var result = Multiplicative();

        while (true)
        {
            if (Match(TokenType.PLUS))
            {
                result = new BinaryExpression(result, Multiplicative(), '+');
                continue;
            }
            if (Match(TokenType.MINUS))
            {
                result = new BinaryExpression(result, Multiplicative(), '-');
                continue;
            }
            break;
        }

        return result;
    }

    private IExpression Multiplicative()
    {
        var result = Unary();

        while (true)
        {
            if (Match(TokenType.STAR))
            {
                result = new BinaryExpression(result, Unary(), '*');
                continue;
            }
            if (Match(TokenType.SLASH))
            {
                result = new BinaryExpression(result, Unary(), '/');
                continue;
            }
            if (Match(TokenType.POWER))
            {
                result = new BinaryExpression(result, Unary(), '^');
                continue;
            }
            break;
        }

        return result;
    }

    private IExpression Unary()
    {
        if (Match(TokenType.PLUS)) return new UnaryExpression(Primary(), '+');
        if (Match(TokenType.MINUS)) return new UnaryExpression(Primary(), '-');
        return Primary();
    }

    private IExpression Primary()
    {
        var current = Get(0);

        if (Match(TokenType.NUMBER)) return new ValueExpression(float.Parse(current.Value, System.Globalization.CultureInfo.InvariantCulture));
        if (Match(TokenType.OPEN_RND_BKT))
        {
            var result = Expression();
            Match(TokenType.CLOSE_RND_BKT);
            return result;
        }
        if (Match(TokenType.WORD)) return new VariableExpression(Get(-1).Value);
        if (Match(TokenType.TEXT)) return new ValueExpression(current.Value);
        if (Match(TokenType.TRUE)) return new ValueExpression(true);
        if (Match(TokenType.FALSE)) return new ValueExpression(false);

        throw new Exception("unknown expression.");
    }

    private bool Match(TokenType type)
    {
        var current = Get(0);
        if (current.Type != type) return false;
        pos++;
        return true;
    }

    private bool LookMatch(TokenType type, int relativePosition) => Get(relativePosition).Type == type;

    private Token Get(int relativePosition)
    {
        var position = pos + relativePosition;
        if (position >= tokens.Count) return EOF;
        return tokens[position];
    }

    private Token Consume(TokenType type)
    {
        var current = Get(0);
        if (current.Type != type) throw new Exception("unexpected token.");
        pos++;
        return current;
    }
}
