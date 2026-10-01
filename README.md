# Tamizhi-lang

Tamizhi is a small Tamil-inspired programming language interpreter built with C# and .NET. It reads `.tam` source files, tokenizes them, parses statements and expressions, and executes them through the interpreter.

## Features

- Tamil keywords and identifiers
- Number and string values
- Variables and arithmetic expressions
- Comparisons and equality operators
- `if` / `else` conditional statements
- Print statements

## Requirements

- .NET 9 SDK

## Run a program

From the project directory, run:

```bash
dotnet run -- Hello.tam
```

The sample program in `Hello.tam` demonstrates output, variables, arithmetic, and conditional logic.

## Project structure

- `Lexer.cs` — converts source text into tokens
- `Parser.cs` — builds executable statements and expressions
- `Interpreter.cs` — executes parsed programs
- `Hello.tam` — sample Tamizhi program
