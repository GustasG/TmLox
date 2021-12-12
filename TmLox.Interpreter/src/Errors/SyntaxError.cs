namespace TmLox.Interpreter.Errors;

public class SyntaxError : LoxError
{
    public SyntaxError(int line, int column, string text)
        : base($"Syntax Error: ({line}:{column}): {text}")
    {
    }

    public override string ToString()
    {
        return Message;
    }
}