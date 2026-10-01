public class Lexer
{
    private readonly string _source;

    public Lexer(string source)
    {
        _source = source;
    }

    public List<Token> Tokenize()
    {
        var tokens = new List<Token>();

        int i = 0;

        while (i < _source.Length)
        {
            // Skip whitespace
            if (char.IsWhiteSpace(_source[i]))
            {
                i++;
                continue;
            }

            // எண்
            if (Matches(i, 0x0B8E, 0x0BA3, 0x0BCD))
            {
                tokens.Add(
                    new Token(TokenType.NumberKeyword, "\u0B8E\u0BA3\u0BCD")
                );

                i += "\u0B8E\u0BA3\u0BCD".Length;
                continue;
            }

            if (Matches(i, 0x0B8E, 0x0BA9, 0x0BCD, 0x0BB1, 0x0BBE, 0x0BB2, 0x0BCD))
            {
                tokens.Add(new Token(TokenType.If, "என்றால்"));
                i += 7;
                continue;
            }

            if (Matches(i, 0x0B87, 0x0BB2, 0x0BCD, 0x0BB2, 0x0BC8, 0x0BAF, 0x0BC6, 0x0BA9, 0x0BBF, 0x0BB2, 0x0BCD))
            {
                tokens.Add(new Token(TokenType.Else, "இல்லையெனில்"));
                i += 11;
                continue;
            }

            if (Matches(i, 0x0B89, 0x0BA3, 0x0BCD, 0x0BAE, 0x0BC8))
            {
                tokens.Add(new Token(TokenType.True, "உண்மை"));
                i += 5;
                continue;
            }

            if (Matches(i, 0x0BAA, 0x0BCA, 0x0BAF, 0x0BCD))
            {
                tokens.Add(new Token(TokenType.False, "பொய்"));
                i += 4;
                continue;
            }

            // அச்சிடு
            //
            if (Matches(i, 0x0B85, 0x0B9A, 0x0BCD, 0x0B9A, 0x0BBF, 0x0B9F, 0x0BC1))
            {
                tokens.Add(
                    new Token(TokenType.Print, "\u0B85\u0B9A\u0BCD\u0B9A\u0BBF\u0B9F\u0BC1")
                );

                i += "\u0B85\u0B9A\u0BCD\u0B9A\u0BBF\u0B9F\u0BC1".Length;
                continue;
            }

            // Identifier
            if (char.IsLetter(_source[i]))
            {
                int start = i;

                while (
                    i < _source.Length &&
                    (char.IsLetterOrDigit(_source[i]) || _source[i] == '_')
                )
                {
                    i++;
                }

                string value = _source.Substring(start, i - start);

                tokens.Add(
                    new Token(TokenType.Identifier, value)
                );

                continue;
            }

            // Number
            if (char.IsDigit(_source[i]))
            {
                int start = i;

                while (
                    i < _source.Length &&
                    char.IsDigit(_source[i])
                )
                {
                    i++;
                }

                string value = _source.Substring(start, i - start);

                tokens.Add(
                    new Token(TokenType.Number, value)
                );

                continue;
            }

            // (
            if (_source[i] == '(')
            {
                tokens.Add(
                    new Token(TokenType.LeftParenthesis, "(")
                );

                i++;
                continue;
            }

            // )
            if (_source[i] == ')')
            {
                tokens.Add(
                    new Token(TokenType.RightParenthesis, ")")
                );

                i++;
                continue;
            }

            // ==
            if (_source[i] == '=' && i + 1 < _source.Length && _source[i + 1] == '=')
            {
                tokens.Add(new Token(TokenType.EqualEqual, "=="));
                i += 2;
                continue;
            }

            // =
            if (_source[i] == '=')
            {
                tokens.Add(
                    new Token(TokenType.Equals, "=")
                );

                i++;
                continue;
            }

            if (_source[i] == '!' && i + 1 < _source.Length && _source[i + 1] == '=')
            {
                tokens.Add(new Token(TokenType.NotEqual, "!="));
                i += 2;
                continue;
            }

            if (_source[i] == '>' && i + 1 < _source.Length && _source[i + 1] == '=')
            {
                tokens.Add(new Token(TokenType.GreaterThanOrEqual, ">="));
                i += 2;
                continue;
            }

            if (_source[i] == '<' && i + 1 < _source.Length && _source[i + 1] == '=')
            {
                tokens.Add(new Token(TokenType.LessThanOrEqual, "<="));
                i += 2;
                continue;
            }

            if (_source[i] == '>')
            {
                tokens.Add(new Token(TokenType.GreaterThan, ">"));
                i++;
                continue;
            }

            if (_source[i] == '<')
            {
                tokens.Add(new Token(TokenType.LessThan, "<"));
                i++;
                continue;
            }

            if (_source[i] == '{')
            {
                tokens.Add(new Token(TokenType.LeftBrace, "{"));
                i++;
                continue;
            }

            if (_source[i] == '}')
            {
                tokens.Add(new Token(TokenType.RightBrace, "}"));
                i++;
                continue;
            }

            // ;
            if (_source[i] == ';')
            {
                tokens.Add(
                    new Token(TokenType.Semicolon, ";")
                );

                i++;
                continue;
            }

            if (_source[i] == '+')
            {
                tokens.Add(new Token(TokenType.Plus, "+"));
                i++;
                continue;
            }

            if (_source[i] == '-')
            {
                tokens.Add(new Token(TokenType.Minus, "-"));
                i++;
                continue;
            }

            if (_source[i] == '*')
            {
                tokens.Add(new Token(TokenType.Multiply, "*"));
                i++;
                continue;
            }

            if (_source[i] == '/')
            {
                tokens.Add(new Token(TokenType.Divide, "/"));
                i++;
                continue;
            }

            // String
            if (_source[i] == '"')
            {
                i++;

                int start = i;

                while (
                    i < _source.Length &&
                    _source[i] != '"'
                )
                {
                    i++;
                }

                string value = _source.Substring(
                    start,
                    i - start
                );

                tokens.Add(
                    new Token(TokenType.String, value)
                );

                if (i < _source.Length)
                {
                    i++;
                }

                continue;
            }

            Console.WriteLine(
                $"Unknown character: {_source[i]}"
            );

            i++;
        }

        return tokens;
    }

    private bool Matches(int position, params int[] codePoints)
    {
        if (position + codePoints.Length > _source.Length)
            return false;

        for (int offset = 0; offset < codePoints.Length; offset++)
        {
            if (_source[position + offset] != codePoints[offset])
                return false;
        }

        return true;
    }
}
