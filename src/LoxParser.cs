using TmLox.Ast;
using TmLox.Errors;
using TmLox.Ast.Statements;
using TmLox.Ast.Expressions;
using System.Collections.Generic;
using TmLox.Ast.Expressions.Literal;

namespace TmLox
{
    public class LoxParser
    {
        private readonly IEnumerator<Token> _tokenStream;

        public LoxParser(IEnumerator<Token> tokenStream)
        {
            _tokenStream = tokenStream;
            _tokenStream.MoveNext();
        }

        public Program Run()
        {
            var statements = new List<Statement>();

            while (!Accept(TokenType.Eof))
            {
                statements.Add(ParseDeclaration());
            }

            return new Program(statements);
        }

        private Statement ParseDeclaration()
        {
            if (Accept(TokenType.KwVar))
                return ParseVariableDeclaration();

            return ParseStatement();
        }

        private Statement ParseStatement()
        {
            throw new ParserException("TODO");
        }

        private Statement ParseVariableDeclaration()
        {
            var name = Expect(TokenType.Identifier, "Identifier");
            Expression? value = null;

            if (Accept(TokenType.OpAssign))
                value = ParseExpression();

            Expect(TokenType.OpSemicolon, ";");
            return new VariableStatement(name, value);
        }

        private Expression ParseExpression()
        {
            return ParseAssigment();
        }

        private Expression ParseAssigment()
        {
            var expression = ParseOr();

            return expression;
        }

        private Expression ParseOr()
        {
            var expression = ParseAnd();

            return expression;
        }

        private Expression ParseAnd()
        {
            var expression = ParseEquality();

            return expression;
        }

        private Expression ParseEquality()
        {
            var expression = ParseComparision();

            return expression;
        }

        private Expression ParseComparision()
        {
            var expression = ParseTerm();

            return expression;
        }

        private Expression ParseTerm()
        {
            var expression = ParseFactor();

            return expression;
        }

        private Expression ParseFactor()
        {
            var expression = ParseUnary();

            return expression;
        }

        private Expression ParseUnary()
        {
            return ParseCall();
        }

        private Expression ParseCall()
        {
            return ParsePrimary();
        }

        private Expression ParsePrimary()
        {
            if (Accept(TokenType.KwNil))
                return new NullLiteralExpression();
            else if (Accept(TokenType.KwFalse))
                return new BoolLiteralExpression(false);
            else if (Accept(TokenType.KwTrue))
                return new BoolLiteralExpression(true);
            else if (Accept(TokenType.LitInt, out var intToken))
                return new IntLiteralExpression(intToken);
            else if (Accept(TokenType.LitFloat, out var floatToken))
                return new FloatLiteralExpression(floatToken);
            else if (Accept(TokenType.LitString, out var stringToken))
                return new StringLiteralExpression(stringToken);
            else if (Accept(TokenType.Identifier, out var identifierToken))
                return new VariableExpression(identifierToken);

            throw new ParserException("Expected expression");
        }

        private bool Accept(TokenType tokenType, out Token value)
        {
            var current = Current();

            if (current.TokenType == tokenType)
            {
                Advance();
                value = current;
                return true;
            }

            value = default;
            return false;
        }

        private bool Accept(TokenType tokenType)
        {
            return Accept(tokenType, out var _);
        }

        private Token Expect(TokenType tokenType, string expected)
        {
            return Accept(tokenType, out var token) ? token : throw new ParserException(Current(), expected);
        }

        private Token Current()
        {
            return _tokenStream.Current;
        }

        private void Advance()
        {
            _tokenStream.MoveNext();
        }

        private bool IsEnd()
        {
            return _tokenStream.Current.TokenType == TokenType.Eof;
        }
    }
}
