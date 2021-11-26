namespace TmLox
{
    public class Token
    {
        public int Line { get; }

        public int Column { get; }

        public Lexeme Lexeme { get; }

        public AnyValue Value { get; }


        public Token(int line, int column, Lexeme lexeme)
        {
            Line = line;
            Column = column;
            Lexeme = lexeme;
            Value = AnyValue.CreateNull();
        }

        public Token(int line, int column, Lexeme lexeme, AnyValue value)
        {
            Line = line;
            Column = column;
            Lexeme = lexeme;
            Value = value;
        }
    }
}