public sealed class IfStatement : Statement
{
    public Expression Condition { get; }
    public List<Statement> ThenBranch { get; }
    public List<Statement>? ElseBranch { get; }

    public IfStatement(Expression condition, List<Statement> thenBranch, List<Statement>? elseBranch)
    {
        Condition = condition;
        ThenBranch = thenBranch;
        ElseBranch = elseBranch;
    }
}
