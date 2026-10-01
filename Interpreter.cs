public class Interpreter
{
    private readonly Environment _environment;

    public Interpreter()
    {
        _environment = new Environment();
    }

    public void Execute(List<Statement> statements)
    {
        foreach (Statement statement in statements)
        {
            ExecuteStatement(statement);
        }
    }

    private void ExecuteStatement(Statement statement)
    {
        if (statement is VariableStatement variable)
        {
            _environment.SetVariable(
                variable.Name,
                Evaluate(variable.ValueExpression)
            );

            return;
        }

        if (statement is PrintStatement print)
        {
            Console.WriteLine(print.Expression is null
                ? print.StringValue
                : Evaluate(print.Expression));
        }

        if (statement is IfStatement conditional)
        {
            if (Convert.ToBoolean(Evaluate(conditional.Condition)))
                Execute(conditional.ThenBranch);
            else if (conditional.ElseBranch is not null)
                Execute(conditional.ElseBranch);
        }
    }

    private object Evaluate(Expression expression)
    {
        if (expression is NumberExpression number)
            return number.Value;

        if (expression is BooleanExpression boolean)
            return boolean.Value;

        if (expression is VariableExpression variable)
            return Convert.ToInt32(_environment.GetVariable(variable.Name));

        BinaryExpression binary = (BinaryExpression)expression;
        int left = Convert.ToInt32(Evaluate(binary.Left));
        int right = Convert.ToInt32(Evaluate(binary.Right));

        return binary.Operator switch
        {
            TokenType.Plus => left + right,
            TokenType.Minus => left - right,
            TokenType.Multiply => left * right,
            TokenType.Divide => right == 0
                ? throw new DivideByZeroException()
                : left / right,
            TokenType.GreaterThan => left > right,
            TokenType.LessThan => left < right,
            TokenType.GreaterThanOrEqual => left >= right,
            TokenType.LessThanOrEqual => left <= right,
            TokenType.EqualEqual => left == right,
            TokenType.NotEqual => left != right,
            _ => throw new Exception("Unknown arithmetic operator")
        };
    }
}
