namespace TmLox.Errors
{
    public class LexerException : LoxException
    {
        public LexerException()
        {
        }

        public LexerException(int line, int column, string message)
            : base($"({line}:{column}): {message}")
        {
        }
    }
}