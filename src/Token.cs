namespace TmLox
{
    public class Token
    {
        public int Line { get; set; }

        public int Column { get; set; }

        public Lexeme Lexeme { get; set; }

        public object? Value { get; set; } // Possible values: long, double, string, null


        public Token(int line, int column, Lexeme lexeme, object? value = null)
        {
            Line = line;
            Column = column;
            Lexeme = lexeme;
            Value = value;
        }
    }
}
