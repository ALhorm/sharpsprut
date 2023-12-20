namespace Sprut;

internal enum TokenType
{
    NUMBER,
    WORD,
    TEXT,

    TRUE,
    FALSE,
    IF,
    ELSE,
    ELIF,
    WHILE,
    LOG,
    FOR,
    VOID,
    FUN,
    RETURN,

    PLUS,
    MINUS,
    STAR,
    SLASH,
    ASSIGN,
    POWER,
    PLUS_ASSIGN,
    MINUS_ASSIGN,
    STAR_ASSIGN,
    SLASH_ASSIGN,

    GREATER,
    LESS,
    EQUAL,
    GREATER_EQUAL,
    LESS_EQUAL,

    OPEN_RND_BKT,
    CLOSE_RND_BKT,
    OPEN_CRL_BKT,
    CLOSE_CRL_BKT,
    OPEN_SQR_BKT,
    CLOSE_SQR_BKT,
    COMMA,

    EOF
}
