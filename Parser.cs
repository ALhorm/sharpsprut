﻿using Sprut.Ast;

namespace Sprut;

internal class Parser
{
    private List<Token> tokens;
    private int pos = 0;
    private Token EOF = new Token(TokenType.EOF, "\0");
    public static string? structure = null;
    private string? instance = null;
    private bool isPublic = false;
    public List<IStatement> publicStatements { get; private set; } = new List<IStatement>();

    public Parser(List<Token> tokens) => this.tokens = tokens;

    public List<IStatement> Parse()
    {
        var result = new List<IStatement>();
        while (!Match(TokenType.EOF)) result.Add(Statement());
        return result;
    }

    private IStatement Statement()
    {
        if (LookMatch(TokenType.DOT, 1)) return new DotStatement((DotExpression)Dot());
        if (Match(TokenType.IF)) return If();
        if (Match(TokenType.WHILE)) return While();
        if (Match(TokenType.LOG)) return new LogStatement(Expression());
        if (Match(TokenType.FOR)) return For();
        if (Match(TokenType.PUBLIC)) return Public();
        if (Match(TokenType.CONST))
        {
            if (Match(TokenType.FUN)) return Function(true);
            if (Match(TokenType.STRUCT)) return Structure(true);
            return Variable(true);
        }
        if (Match(TokenType.FUN)) return Function(false);
        if (Match(TokenType.RETURN)) return new ReturnStatement(Expression());
        if (Match(TokenType.STRUCT)) return Structure(false);
        if (LookMatch(TokenType.WORD, 0) && LookMatch(TokenType.OPEN_RND_BKT, 1))
            return new FunctionCallStatement(FunctionCall());

        return Variable(false);
    }

    private IStatement Variable(bool isConst)
    {
        var isStruct = false;
        if (Match(TokenType.THIS))
        {
            Consume(TokenType.DOT);
            isStruct = true;
        }

        var name = Consume(TokenType.WORD).Value;
        var operation = Get(0);
        Consume(operation.Type);

        if (LookMatch(TokenType.NEW, 0)) instance = name;

        return new VariableStatement(name, operation.Value, structure, isConst, isStruct, Expression());

        throw new Exception("unknown statement.");
    }

    private BlockStatement Block()
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

    private IStatement For()
    {
        var init = Statement();
        Consume(TokenType.COMMA);
        var condition = Expression();
        Consume(TokenType.COMMA);
        var action = Statement();

        return new ForStatement(init, condition, action, BlockOrStatement());
    }

    private IStatement Function(bool isConst)
    {
        var args = new Dictionary<string, IExpression?>();
        var name = Consume(TokenType.WORD).Value;

        Consume(TokenType.OPEN_RND_BKT);

        while (!Match(TokenType.CLOSE_RND_BKT))
        {
            var arg_name = Consume(TokenType.WORD).Value;
            IExpression? arg_value = null;

            if (Match(TokenType.ASSIGN)) arg_value = Expression();
            args.Add(arg_name, arg_value);

            Match(TokenType.COMMA);
        }

        return new FunctionStatement(name, args, isConst, isPublic, BlockOrStatement());
    }

    private IStatement Structure(bool isConst)
    {
        var name = Consume(TokenType.WORD).Value;
        structure = name;
        return new StructStatement(name, isConst, Block());
    }

    private IStatement Public()
    {
        isPublic = true;
        var result = Statement();
        publicStatements.Add(result);
        isPublic = false;

        return result;
    }

    private IExpression This()
    {
        Consume(TokenType.DOT);
        var name = Consume(TokenType.WORD).Value;
        return new ThisExpression(name);
    }

    private FunctionCallExpression FunctionCall()
    {
        var name = Consume(TokenType.WORD).Value;
        var args = new Dictionary<Dictionary<int, string?>, IExpression>();

        Consume(TokenType.OPEN_RND_BKT);
        for (int i = 0; !Match(TokenType.CLOSE_RND_BKT); i++)
        {
            string? argName = null;

            if (LookMatch(TokenType.ASSIGN, 1))
            {
                argName = Consume(TokenType.WORD).Value;
                Consume(TokenType.ASSIGN);
            }

            var argKey = new Dictionary<int, string?>() { [i] = argName };
            args[argKey] = Expression();
            Match(TokenType.COMMA);
        }

        return new FunctionCallExpression(name, args);
    }

    private IExpression ListAccessExpression(IExpression expression)
    {
        var indices = new List<IExpression>();

        while (Match(TokenType.OPEN_SQR_BKT))
        {
            indices.Add(Expression());
            Consume(TokenType.CLOSE_SQR_BKT);
        }

        return new ListAccessExpression(expression, indices);
    }

    private IExpression ListExpression()
    {
        var result = new List<IExpression>();

        while (!Match(TokenType.CLOSE_SQR_BKT))
        {
            result.Add(Expression());
            Match(TokenType.COMMA);
        }

        return new ListExpression(result);
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
        if (Match(TokenType.PLUS)) return new UnaryExpression(Dot(), '+');
        if (Match(TokenType.MINUS)) return new UnaryExpression(Dot(), '-');
        return Dot();
    }

    private IExpression Dot()
    {
        var result = ListAccess();

        if (LookMatch(TokenType.DOT, 0) && !LookMatch(TokenType.THIS, -1))
        {
            var expression = result;
            Consume(TokenType.DOT);
            if (LookMatch(TokenType.OPEN_RND_BKT, 1)) return new DotExpression(expression, FunctionCall(), null);
        }

        return result;
    }

    private IExpression ListAccess()
    {
        var result = Primary();
        if (!LookMatch(TokenType.ASSIGN, -1) && LookMatch(TokenType.OPEN_SQR_BKT, 0)) return ListAccessExpression(result);

        return result;
    }

    private IExpression Primary()
    {
        var current = Get(0);

        if (Match(TokenType.NUMBER)) return new ValueExpression(float.Parse(current.Value, System.Globalization.CultureInfo.InvariantCulture));
        if (Match(TokenType.NEW)) return new NewExpression(FunctionCall(), instance);
        if (LookMatch(TokenType.WORD, 0) && LookMatch(TokenType.OPEN_RND_BKT, 1)) return FunctionCall();
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
        if (Match(TokenType.OPEN_SQR_BKT)) return ListExpression();
        if (Match(TokenType.VOID)) return new ValueExpression('v');
        if (Match(TokenType.THIS)) return This();

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
        if (current.Type != type) throw new Exception($"unexpected token '{current.Value}'.");
        pos++;
        return current;
    }
}
