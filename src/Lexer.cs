using System.Text;
using System.Collections.Generic;

using TmLox.Errors;

namespace TmLox
{
    public class Lexer : ILexer
    {
        public Token? Current { get; private set; }

        private static readonly Dictionary<string, Lexeme> _keywords = new()
        {
            { "and", Lexeme.KwAnd },
            { "break", Lexeme.KwBreak },
            { "elif", Lexeme.KwElif },
            { "else", Lexeme.KwElse },
            { "false", Lexeme.KwFalse },
            { "true", Lexeme.KwTrue },
            { "for", Lexeme.KwFor },
            { "fun", Lexeme.KwFun },
            { "if", Lexeme.KwIf },
            { "nil", Lexeme.KwNil },
            { "or", Lexeme.KwOr },
            { "return", Lexeme.KwReturn },
            { "var", Lexeme.KwVar },
            { "while", Lexeme.KwWhile }
        };

        private readonly string _source;
        private int _position;
        private int _line;
        private int _column;
        private bool _finished;


        public Lexer(string source)
        {
            _source = source;
            Reset();
            Next();
        }

        public void Reset()
        {
            _position = 0;
            _line = 1;
            _column = 0;
            _finished = false;
        }

        public Token? Next()
        {
            if (!Finished())
            {
                Current = FetchToken();
                _finished = Current.Lexeme == Lexeme.Eof;
            }
            else
            {
                Current = null;
            }
            
            return Current;
        }

        public bool Finished()
        {
            return _finished;
        }

        private Token FetchToken()
        {
            while (!IsEnd())
            {
                var c = Advance();

                switch (c)
                {
                    case ' ':
                    case '\t':
                    case '\r':
                        break;
                    case '#':
                        SkipLine();
                        Newline();
                        break;
                    case '\n':
                        Newline();
                        break;
                    default:
                        return CreateToken(c);
                }
            }

            return new Token(_line, _column + 1, Lexeme.Eof);
        }

        private Token CreateToken(char startingChar)
        {
            switch (startingChar)
            {
                // Syntax
                case ',': return new Token(_line, _column, Lexeme.OpComma);
                case ';': return new Token(_line, _column, Lexeme.OpSemicolon);

                // Braces
                case '(': return new Token(_line, _column, Lexeme.OPLParen);
                case ')': return new Token(_line, _column, Lexeme.OpRParen);
                case '{': return new Token(_line, _column, Lexeme.OpLBrace);
                case '}': return new Token(_line, _column, Lexeme.OPRBrace);

                // Math
                case '+': return CreatePlusToken();
                case '-': return CreateMinusToken();
                case '*': return CreateMulToken();
                case '/': return CreateDivToken();
                case '%': return CreateModToken();

                // Logical
                case '=': return CreateEqualToken();
                case '!': return CreateExclamationToken();
                case '<': return CreateLessToken();
                case '>': return CreateMoreToken();

                // Other
                case '"': return CreateStringToken();

                default:
                    if (char.IsDigit(startingChar))
                        return CreateNumberToken();
                    else if (char.IsLetter(startingChar) || startingChar == '_')
                        return CreateIdentifierToken();
                    else
                        throw new SyntaxError(_line, _column, $"Unexpected characted: {startingChar}");
            }
        }

        private Token CreatePlusToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpPlusEq);
            else
                return new Token(_line, _column, Lexeme.OpPlus);
        }

        private Token CreateMinusToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpMinusEq);
            else
                return new Token(_line, _column, Lexeme.OpMinus);
        }

        private Token CreateMulToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpMulEq);
            else
                return new Token(_line, _column, Lexeme.OpMul);
        }

        private Token CreateDivToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpDiv);
            else
                return new Token(_line, _column, Lexeme.OpDivEq);
        }

        private Token CreateModToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpModEq);
            else
                return new Token(_line, _column, Lexeme.OpMod);
        }

        private Token CreateEqualToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpEq);
            else
                return new Token(_line, _column, Lexeme.OpAssign);
        }

        private Token CreateExclamationToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpNotEqual);
            else
                return new Token(_line, _column, Lexeme.OpExclamation);
        }

        private Token CreateLessToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpLessEq);
            else
                return new Token(_line, _column, Lexeme.OpLess);
        }

        private Token CreateMoreToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, Lexeme.OpMoreEq);
            else
                return new Token(_line, _column, Lexeme.OpMore);
        }

        private Token CreateStringToken()
        {
            int startingLine = _line;
            int startingColumn = _column;
            var sb = new StringBuilder();

            while (!TryConsume('"'))
            {
                if (IsEnd())
                    throw new SyntaxError(startingLine, startingColumn, "Unterminated string");

                var currentChar = Advance();

                switch (currentChar)
                {
                    case '\\':
                        char escapeSymbol = CreateEscapeSymbol(Advance());
                        sb.Append(escapeSymbol);
                        break;
                    case '\n':
                        Newline();
                        sb.Append(currentChar);
                        break;
                    default:
                        sb.Append(currentChar);
                        break;
                }
            }

            return new Token(startingLine, startingColumn, Lexeme.LitString, AnyValue.CreateString(sb.ToString()));
        }

        private char CreateEscapeSymbol(char symbol)
        {
            return symbol switch
            {
                '\'' => '\'',
                '\"' => '"',
                '\\' => '\\',
                'a' => '\a',
                'b' => '\b',
                'f' => '\f',
                'n' => '\n',
                'r' => '\r',
                't' => '\t',
                'v' => '\v',
                _ => throw new SyntaxError(_line, _column - 1, $"Invalid escape symbol: \\{symbol}"),
            };
        }

        private Token CreateNumberToken()
        {
            int startingColumn = _column;

            var value = CreateNumber();
            value = value.Replace('.', ',');

            if (long.TryParse(value, out var integer))
                return new Token(_line, startingColumn, Lexeme.LitInt, AnyValue.CreateInteger(integer));
            else if (double.TryParse(value, out var floatingPoint))
                return new Token(_line, startingColumn, Lexeme.LitFloat, AnyValue.CreateFloat(floatingPoint));
            else
                throw new SyntaxError(_line, startingColumn, "Invalid number");
        }

        private string CreateNumber()
        {
            int startingPosition = _position - 1;

            while (!IsEnd())
            {
                char currentSymbol = Peek();

                if (char.IsDigit(currentSymbol) || currentSymbol == '.')
                    Advance();
                else
                    break;
            }

            return _source[startingPosition.._position];
        }

        private Token CreateIdentifierToken()
        {
            int startingColumn = _column;
            var value = CreateIdentifier();

            return _keywords.TryGetValue(value, out var lexeme)
                ? new Token(_line, startingColumn, lexeme)
                : new Token(_line, startingColumn, Lexeme.Identifier, AnyValue.CreateString(value));
        }

        private string CreateIdentifier()
        {
            int startingPosition = _position - 1;

            while (!IsEnd())
            {
                char currentSymbol = Peek();

                if (char.IsLetterOrDigit(currentSymbol) || currentSymbol == '_')
                    Advance();
                else
                    break;
            }

            return _source[startingPosition.._position];
        }

        private void SkipLine()
        {
            while (!IsEnd() && !TryConsume('\n'))
            {
                Advance();
            }
        }

        private void Newline()
        {
            _line += 1;
            _column = 0;
        }

        private char Advance()
        {
            _column += 1;

            return _source[_position++];
        }

        private char Peek()
        {
            return _source[_position];
        }

        private bool TryConsume(char testChar)
        {
            if (!IsEnd() && Peek() == testChar)
            {
                Advance();
                return true;
            }

            return false;
        }

        private bool IsEnd()
        {
            return _position >= _source.Length;
        }
    }
}