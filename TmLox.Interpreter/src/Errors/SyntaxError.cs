namespace TmLox.Errors
{
    public class SyntaxError : LoxError
    {
        public int Line { get; }

        public int Column { get; }

        public string Text { get; }


        public SyntaxError(int line, int column, string text)
        {
            Line = line;
            Column = column;
            Text = text;
        }

        public SyntaxError(Token token, string text)
            : this(token.Line, token.Column, text)
        {
        }

        public override string ToString()
        {
            return $"Syntax Error: ({Line}:{Column}): {Text}";
        }
    }
}