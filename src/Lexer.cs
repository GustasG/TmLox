using System.Text;
using System.Collections;
using System.Collections.Generic;

using TmLox.Errors;

namespace TmLox
{
    public class Lexer : IEnumerator<Token>, IEnumerable<Token>
    {
        private static readonly Dictionary<string, TokenType> _keywords = new()
        {
            { "and", TokenType.KwAnd },
            { "break", TokenType.KwBreak },
            { "elif", TokenType.KwElif },
            { "else", TokenType.KwElse },
            { "false", TokenType.KwFalse },
            { "true", TokenType.KwTrue },
            { "for", TokenType.KwFor },
            { "fun", TokenType.KwFun },
            { "if", TokenType.KwIf },
            { "nil", TokenType.KwNil },
            { "or", TokenType.KwOr },
            { "return", TokenType.KwReturn },
            { "var", TokenType.KwVar },
            { "while", TokenType.KwWhile }
        };

        private readonly string _source;
        private int _position;
        private int _line;
        private int _column;
        private bool _finished;

        public Token Current { get; private set; }

        public Lexer(string source)
        {
            _source = source;
            Reset();
            MoveNext();
        }

        public void Reset()
        {
            _position = 0;
            _line = 1;
            _column = 0;
            _finished = false;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (!_finished)
            {
                Current = FetchToken();
                _finished = Current.TokenType == TokenType.Eof;

                return true;
            }

            return false;
        }

        public void Dispose()
        {
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
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

            return new Token(_line, _column + 1, TokenType.Eof);
        }

        private Token CreateToken(char startingChar)
        {
            switch (startingChar)
            {
                // Syntax
                case ',': return new Token(_line, _column, TokenType.OpComma);
                case ';': return new Token(_line, _column, TokenType.OpSemicolon);

                // Braces
                case '(': return new Token(_line, _column, TokenType.OPLParen);
                case ')': return new Token(_line, _column, TokenType.OpRParen);
                case '{': return new Token(_line, _column, TokenType.OpLBrace);
                case '}': return new Token(_line, _column, TokenType.OPRBrace);

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
                        throw new LexerException(_line, _column, $"Unexpected characted: {startingChar}");
            }
        }

        private Token CreatePlusToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpPlusEq);
            else
                return new Token(_line, _column, TokenType.OpPlus);
        }

        private Token CreateMinusToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpMinusEq);
            else
                return new Token(_line, _column, TokenType.OpMinus);
        }

        private Token CreateMulToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpMulEq);
            else
                return new Token(_line, _column, TokenType.OpMul);
        }

        private Token CreateDivToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpDiv);
            else
                return new Token(_line, _column, TokenType.OpDivEq);
        }

        private Token CreateModToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpModEq);
            else
                return new Token(_line, _column, TokenType.OpMod);
        }

        private Token CreateEqualToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpEq);
            else
                return new Token(_line, _column, TokenType.OpAssign);
        }

        private Token CreateExclamationToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpNotEqual);
            else
                return new Token(_line, _column, TokenType.OpExclamation);
        }

        private Token CreateLessToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpLessEq);
            else
                return new Token(_line, _column, TokenType.OpLess);
        }

        private Token CreateMoreToken()
        {
            if (TryConsume('='))
                return new Token(_line, _column - 1, TokenType.OpMoreEq);
            else
                return new Token(_line, _column, TokenType.OpMore);
        }

        private Token CreateStringToken()
        {
            int startingLine = _line;
            int startingColumn = _column;
            var sb = new StringBuilder();

            while (!TryConsume('"'))
            {
                if (IsEnd())
                    throw new LexerException(startingLine, startingColumn, "Unterminated string");

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

            return new Token(startingLine, startingColumn, TokenType.LitString, sb.ToString());
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
                _ => throw new LexerException(_line, _column - 1, $"Invalid escape symbol: \\{symbol}"),
            };
        }

        private Token CreateNumberToken()
        {
            int startingColumn = _column;

            var value = CreateNumber();
            value = value.Replace('.', ',');

            if (long.TryParse(value, out var integer))
                return new Token(_line, startingColumn, TokenType.LitInt, integer);
            else if (double.TryParse(value, out var floatingPoint))
                return new Token(_line, startingColumn, TokenType.LitFloat, floatingPoint);
            else
                throw new LexerException(_line, startingColumn, "Invalid number");
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
                : new Token(_line, startingColumn, TokenType.Identifier, value);
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