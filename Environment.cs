public class Environment
{
    private readonly Dictionary<string, object> _variables = new();

    public void SetVariable(string name, object value)
    {
        _variables[name] = value;
    }

    public object GetVariable(string name)
    {
        if (!_variables.ContainsKey(name))
        {
            throw new Exception($"Variable '{name}' is not defined.");
        }
        return _variables[name];
    }
}