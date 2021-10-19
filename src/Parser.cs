using TmLox.Ast;
using TmLox.Errors;
using TmLox.Ast.Statements;
using TmLox.Ast.Expressions;
using System.Collections.Generic;
using TmLox.Ast.Expressions.Literal;

namespace TmLox
{
    public class Parser
    {
        private readonly IEnumerator<Token> _tokenStream;


        public Parser(IEnumerator<Token> tokenStream)
        {
            _tokenStream = tokenStream;
            _tokenStream.MoveNext();
        }

        public List<Statement> Run()
        {
            var statements = new List<Statement>();

            while (!Accept(Lexeme.Eof))
            {
                statements.Add(ParseDeclaration());
            }

            return statements;
        }

        private Statement ParseDeclaration()
        {
            if (Accept(Lexeme.KwVar))
                return ParseVariableDeclaration();

            return ParseStatement();
        }

        private Statement ParseStatement()
        {
            throw new ParserException("TODO");
        }

        private Statement ParseVariableDeclaration()
        {
            var name = Expect(Lexeme.Identifier, "Identifier");
            Expression? value = null;

            if (Accept(Lexeme.OpAssign))
                value = ParseExpression();

            Expect(Lexeme.OpSemicolon, ";");
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
            if (Accept(Lexeme.KwNil))
                return new NullLiteralExpression();
            else if (Accept(Lexeme.KwFalse))
                return new BoolLiteralExpression(false);
            else if (Accept(Lexeme.KwTrue))
                return new BoolLiteralExpression(true);
            else if (Accept(Lexeme.LitInt, out var intToken))
                return new IntLiteralExpression(intToken);
            else if (Accept(Lexeme.LitFloat, out var floatToken))
                return new FloatLiteralExpression(floatToken);
            else if (Accept(Lexeme.LitString, out var stringToken))
                return new StringLiteralExpression(stringToken);
            else if (Accept(Lexeme.Identifier, out var identifierToken))
                return new VariableExpression(identifierToken);

            throw new ParserException("Expected expression");
        }

        private bool Accept(Lexeme lexeme, out Token value)
        {
            var current = Current();

            if (current.Lexeme == lexeme)
            {
                Advance();
                value = current;
                return true;
            }

            value = null;
            return false;
        }

        private bool Accept(Lexeme lexeme)
        {
            return Accept(lexeme, out var _);
        }

        private Token Expect(Lexeme lexeme, string expected)
        {
            return Accept(lexeme, out var token) ? token : throw new ParserException(Current(), expected);
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
            return _tokenStream.Current.Lexeme == Lexeme.Eof;
        }
    }
}
