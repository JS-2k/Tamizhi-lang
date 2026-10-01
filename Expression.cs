public abstract class Expression
{
}

public sealed class NumberExpression : Expression
{
    public int Value { get; }

    public NumberExpression(int value)
    {
        Value = value;
    }
}

public sealed class BooleanExpression : Expression
{
    public bool Value { get; }

    public BooleanExpression(bool value)
    {
        Value = value;
    }
}

public sealed class VariableExpression : Expression
{
    public string Name { get; }

    public VariableExpression(string name)
    {
        Name = name;
    }
}

public sealed class BinaryExpression : Expression
{
    public Expression Left { get; }
    public TokenType Operator { get; }
    public Expression Right { get; }

    public BinaryExpression(Expression left, TokenType @operator, Expression right)
    {
        Left = left;
        Operator = @operator;
        Right = right;
    }
}
