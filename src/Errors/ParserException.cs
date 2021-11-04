namespace TmLox.Errors
{
    public class ParserException : LoxException
    {
        public ParserException()
        {
        }

        public ParserException(string message)
            : base(message)
        {
        }

        public ParserException(int line, int column, string text)
            : this($"({line}:{column}): Expected: \"{text}\"")
        {
        }

        public ParserException(Token token, string text)
            : this(token.Line, token.Column, text)
        {
        }
    }
}