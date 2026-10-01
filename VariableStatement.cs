public class VariableStatement : Statement
{
    public string Name { get; }
    public Expression ValueExpression { get; }

    public VariableStatement(string name, int value)
    {
        Name = name;
        ValueExpression = new NumberExpression(value);
    }

    public VariableStatement(string name, Expression valueExpression)
    {
        Name = name;
        ValueExpression = valueExpression;
    }
}
