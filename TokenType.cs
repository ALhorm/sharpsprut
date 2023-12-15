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

    PLUS,
    MINUS,
    STAR,
    SLASH,
    ASSIGN,
    POWER,

    GREATER,
    LESS,
    EQUAL,
    GREATER_EQUAL,
    LESS_EQUAL,

    OPEN_RND_BKT,
    CLOSE_RND_BKT,
    OPEN_CRL_BKT,
    CLOSE_CRL_BKT,

    EOF
}
