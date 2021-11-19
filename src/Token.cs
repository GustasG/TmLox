namespace TmLox
{
    public class Token
    {
        public int Line { get; set; }

        public int Column { get; set; }

        public Lexeme Lexeme { get; set; }

        public AnyValue Value { get; set; }


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