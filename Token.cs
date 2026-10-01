public enum TokenType
{
    Print,
    NumberKeyword,
    String,
    Number,
    Identifier,
    LeftParenthesis,
    RightParenthesis,
    Equals,
    Semicolon,
    Plus,
    Minus,
    Multiply,
    Divide
    ,GreaterThan
    ,LessThan
    ,GreaterThanOrEqual
    ,LessThanOrEqual
    ,EqualEqual
    ,NotEqual
    ,LeftBrace
    ,RightBrace
    ,If
    ,Else
    ,True
    ,False
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
}
