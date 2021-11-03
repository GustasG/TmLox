namespace TmLox
{
    public class Token
    {
        public int Line { get; set; }

        public int Column { get; set; }

        public TokenType TokenType { get; set; }

        public object? Value { get; set; } // Possible values: long, double, string, null


        public Token(int line, int column, TokenType tokenType, object? value = null)
        {
            Line = line;
            Column = column;
            TokenType = tokenType;
            Value = value;
        }
    }
}
