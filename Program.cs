if (args.Length == 0)
{
    Console.WriteLine("Tamizhi: No source file provided.");
    return;
}

string filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine($"Tamizhi: File not found - {filePath}");
    return;
}

string sourceCode = File.ReadAllText(filePath, System.Text.Encoding.UTF8);

Console.WriteLine("Tamizhi: Compiling source code...");

Lexer lexer = new Lexer(sourceCode);

List<Token> tokens = lexer.Tokenize();

Parser parser = new Parser(tokens);

List<Statement> statements = parser.Parse();

Interpreter interpreter = new Interpreter();

interpreter.Execute(statements);
