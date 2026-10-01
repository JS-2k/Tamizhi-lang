public class PrintStatement : Statement
{
    public string? StringValue { get; }
    public Expression? Expression { get; }

    public PrintStatement(string value, bool isVariable)
    {
        StringValue = isVariable ? null : value;
        Expression = isVariable ? new VariableExpression(value) : null;
    }

    public PrintStatement(Expression expression)
    {
        Expression = expression;
    }
}
