namespace TmLox.Interpreter.Errors
{
    public class SyntaxError : LoxError
    {
        public int Line { get; }

        public int Column { get; }

        public string Text { get; }


        public SyntaxError(int line, int column, string text)
            : base($"Syntax Error: ({line}:{column}): {text}")
        {
        }

        public override string ToString()
        {
            return Message;
        }
    }
}