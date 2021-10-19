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

        public ParserException(Token token, string expected)
            : this($"({token.Line}:{token.Column}): Expected: \"{expected}\"")
        {
        }
    }
}
