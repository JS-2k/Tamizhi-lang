public class Parser
{
    private readonly List<Token> _tokens;
    private int _position = 0;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public List<Statement> Parse()
    {
        var statements = new List<Statement>();

        while (_position < _tokens.Count)
        {
            statements.Add(ParseStatement());
        }

        return statements;
    }

    private Statement ParseStatement()
    {
        Token currentToken = _tokens[_position];

        if (currentToken.Type == TokenType.NumberKeyword)
            return ParseVariableStatement();

        if (currentToken.Type == TokenType.Print)
            return ParsePrintStatement();

        if (currentToken.Type == TokenType.If)
            return ParseIfStatement();

        throw new Exception($"Unexpected token: {currentToken.Value}");
    }

    private IfStatement ParseIfStatement()
    {
        _position++;
        Expect(TokenType.LeftParenthesis, "Expected (");
        Expression condition = ParseExpression();
        Expect(TokenType.RightParenthesis, "Expected )");
        List<Statement> thenBranch = ParseBlock();
        List<Statement>? elseBranch = null;

        if (Match(TokenType.Else))
            elseBranch = ParseBlock();

        return new IfStatement(condition, thenBranch, elseBranch);
    }

    private List<Statement> ParseBlock()
    {
        Expect(TokenType.LeftBrace, "Expected {");
        var statements = new List<Statement>();

        while (_position < _tokens.Count && _tokens[_position].Type != TokenType.RightBrace)
            statements.Add(ParseStatement());

        Expect(TokenType.RightBrace, "Expected }");
        return statements;
    }

    private VariableStatement ParseVariableStatement()
    {
        // எண்
        _position++;

        // variable name
        Token identifier = _tokens[_position];

        if (identifier.Type != TokenType.Identifier)
        {
            throw new Exception("Expected variable name");
        }

        string name = identifier.Value;

        _position++;

        // =
        Token equals = _tokens[_position];

        if (equals.Type != TokenType.Equals)
        {
            throw new Exception("Expected =");
        }

        _position++;

        Expression value = ParseExpression();

        // ;
        Token semicolon = _tokens[_position];

        if (semicolon.Type != TokenType.Semicolon)
        {
            throw new Exception("Expected ;");
        }

        _position++;

        return new VariableStatement(name, value);
    }

    private PrintStatement ParsePrintStatement()
    {
        // அச்சிடு
        _position++;

        // (
        Token leftParen = _tokens[_position];

        if (leftParen.Type != TokenType.LeftParenthesis)
        {
            throw new Exception("Expected (");
        }

        _position++;

        // String or arithmetic expression
        Token valueToken = _tokens[_position];

        if (valueToken.Type == TokenType.String)
        {
            string stringValue = valueToken.Value;
            _position++;
            Expect(TokenType.RightParenthesis, "Expected )");
            Expect(TokenType.Semicolon, "Expected ;");
            return new PrintStatement(stringValue, false);
        }

        Expression expression = ParseExpression();
        Expect(TokenType.RightParenthesis, "Expected )");
        Expect(TokenType.Semicolon, "Expected ;");
        return new PrintStatement(expression);
    }

    private Expression ParseExpression()
    {
        Expression expression = ParseTerm();

        if (Match(TokenType.GreaterThan) || Match(TokenType.LessThan) ||
            Match(TokenType.GreaterThanOrEqual) || Match(TokenType.LessThanOrEqual) ||
            Match(TokenType.EqualEqual) || Match(TokenType.NotEqual))
        {
            TokenType @operator = _tokens[_position - 1].Type;
            expression = new BinaryExpression(expression, @operator, ParseTerm());
        }

        while (Match(TokenType.Plus) || Match(TokenType.Minus))
        {
            TokenType @operator = _tokens[_position - 1].Type;
            expression = new BinaryExpression(expression, @operator, ParseTerm());
        }

        return expression;
    }

    private Expression ParseTerm()
    {
        Expression expression = ParsePrimary();

        while (Match(TokenType.Multiply) || Match(TokenType.Divide))
        {
            TokenType @operator = _tokens[_position - 1].Type;
            expression = new BinaryExpression(expression, @operator, ParsePrimary());
        }

        return expression;
    }

    private Expression ParsePrimary()
    {
        if (_position >= _tokens.Count)
            throw new Exception("Expected expression");

        Token token = _tokens[_position++];

        if (token.Type == TokenType.Number)
            return new NumberExpression(int.Parse(token.Value));

        if (token.Type == TokenType.True)
            return new BooleanExpression(true);

        if (token.Type == TokenType.False)
            return new BooleanExpression(false);

        if (token.Type == TokenType.Identifier)
            return new VariableExpression(token.Value);

        if (token.Type == TokenType.LeftParenthesis)
        {
            Expression expression = ParseExpression();
            Expect(TokenType.RightParenthesis, "Expected )");
            return expression;
        }

        throw new Exception($"Expected number, variable, or ( but found {token.Value}");
    }

    private bool Match(TokenType type)
    {
        if (_position < _tokens.Count && _tokens[_position].Type == type)
        {
            _position++;
            return true;
        }

        return false;
    }

    private void Expect(TokenType type, string message)
    {
        if (!Match(type))
            throw new Exception(message);
    }
}
