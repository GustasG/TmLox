using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Errors;
using TmLox.Ast.Statements;
using TmLox.Ast.Expressions;
using TmLox.Ast.Expressions.Unary;
using TmLox.Ast.Expressions.Literal;
using TmLox.Ast.Expressions.Variable;
using TmLox.Ast.Expressions.Binary.Logical;
using TmLox.Ast.Expressions.Binary.Arithmetic;

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
                statements.Add(ParseStatement());
            }

            return new Program(statements);
        }

        private Statement ParseStatement()
        {
            if (Accept(TokenType.KwVar))
                return ParseVariableStatement();
            else if (Accept(TokenType.KwIf))
                return ParseIfStatement();
            else if (Accept(TokenType.KwFun))
                return ParseFunctionStatement();

            else if (Accept(TokenType.KwReturn))
                return ParseReturnStatement();

            // TODO: NOT FINISHED
            else if (Accept(TokenType.KwClass))
                return ParseClassDeclaration();

            return ParseExpression();
        }

        private VariableStatement ParseVariableStatement()
        {
            var name = Expect(TokenType.Identifier, "Identifier");
            Expression? value = null;

            if (Accept(TokenType.OpAssign))
                value = ParseExpression();

            Expect(TokenType.OpSemicolon, ";");
            return new VariableStatement(name, value);
        }

        private IfStatement ParseIfStatement()
        {
            Expect(TokenType.OPLParen, "(");
            var condition = ParseExpression();
            Expect(TokenType.OpRParen, ")");

            Expect(TokenType.OpLBrace, "{");
            var statements = ParseBlock();
            Expect(TokenType.OPRBrace, "}");

            return new IfStatement(condition, statements);
        }

        private FunctionStatement ParseFunctionStatement()
        {
            var name = Expect(TokenType.Identifier, "Identifier");

            Expect(TokenType.OPLParen, "(");
            var parameters = ParseFunctionParameters();
            Expect(TokenType.OpRParen, ")");

            Expect(TokenType.OpLBrace, "{");
            var body = ParseBlock();
            Expect(TokenType.OPRBrace, "}");

            return new FunctionStatement(name, parameters, body);
        }

        private IList<string> ParseFunctionParameters()
        {
            var parameters = new List<string>();

            while (!Match(TokenType.OpRParen))
            {
                var parameter = Expect(TokenType.Identifier, "Identifier");
                Accept(TokenType.OpComma);

                parameters.Add(parameter.Value as string);
            }

            return parameters;
        }

        private ReturnStatement ParseReturnStatement()
        {
            Expression? value = null;

            if (!Match(TokenType.OpSemicolon))
                value = ParseExpression();

            Expect(TokenType.OpSemicolon, ";");
            return new ReturnStatement(value);
        }

        private IList<Statement> ParseBlock()
        {
            var statements = new List<Statement>();

            while (!Match(TokenType.OPRBrace))
            {
                statements.Add(ParseStatement());
            }

            return statements;
        }

        private ClassStatement ParseClassDeclaration()
        {
            var name = Expect(TokenType.Identifier, "Class name");
            var inherited = new List<string>();

            if (Accept(TokenType.OpLess))
            {
                do
                {
                    var className = Expect(TokenType.Identifier, "Inherited class name");
                    Accept(TokenType.OpComma);

                    inherited.Add(className.Value as string);
                }
                while (!Match(TokenType.OPRBrace));
            }

            Expect(TokenType.OpLBrace, "{");
            // TODO: Handle class body
            Expect(TokenType.OPRBrace, "}");

            return new ClassStatement(name, inherited);
        }

        private Expression ParseExpression()
        {
            return ParseAssigmentExpression();
        }

        private Expression ParseAssigmentExpression()
        {
            var expression = ParseOrExpression();

            if (Match(TokenType.OpAssign, TokenType.OpPlusEq, TokenType.OpMinusEq, TokenType.OpMulEq, TokenType.OpDivEq, TokenType.OpModEq))
            {
                var variableName = expression as VariableExpression;

                if (Accept(TokenType.OpAssign))
                    expression = new VariableAssigmentExpression(variableName, ParseOrExpression());
                else if (Accept(TokenType.OpPlusEq))
                    expression = new VariableAdditionExpression(variableName, ParseOrExpression());
                else if (Accept(TokenType.OpMinusEq))
                    expression = new VariableSubtractionExpression(variableName, ParseOrExpression());
               else if (Accept(TokenType.OpMulEq))
                    expression = new VariableMultiplicationExpression(variableName, ParseOrExpression());
                else if (Accept(TokenType.OpDivEq))
                    expression = new VariableDivisionExpression(variableName, ParseOrExpression());
                else if (Accept(TokenType.OpModEq))
                    expression = new VariableModulusExpression(variableName, ParseOrExpression());

                Expect(TokenType.OpSemicolon, ";");
            }

            return expression;
        }

        private Expression ParseOrExpression()
        {
            var expression = ParseAndExpression();

            while (Accept(TokenType.KwOr))
                expression = new OrExpression(expression, ParseAndExpression());

            return expression;
        }

        private Expression ParseAndExpression()
        {
            var expression = ParseEqualityExpression();

            while (Accept(TokenType.KwAnd))
                expression = new AndExpression(expression, ParseEqualityExpression());

            return expression;
        }

        private Expression ParseEqualityExpression()
        {
            var expression = ParseComparisonExpression();

            while (Match(TokenType.OpEq, TokenType.OpExclamationEq))
            {
                if (Accept(TokenType.OpEq))
                    expression = new EqualExpression(expression, ParseComparisonExpression());
                else if (Accept(TokenType.OpExclamationEq))
                    expression = new NotEqualExpression(expression, ParseComparisonExpression());
            }

            return expression;
        }

        private Expression ParseComparisonExpression()
        {
            var expression = ParseArithmeticExpression1();

            if (Accept(TokenType.OpLess))
                return new LessExpression(expression, ParseArithmeticExpression1());
            else if (Accept(TokenType.OpLessEq))
                return new LessEqualExpression(expression, ParseArithmeticExpression1());
            else if (Accept(TokenType.OpMore))
                return new MoreExpression(expression, ParseArithmeticExpression1());
            else if (Accept(TokenType.OpMoreEq))
                return new MoreEqualExpression(expression, ParseArithmeticExpression1());

            return expression;
        }

        private Expression ParseArithmeticExpression1()
        {
            var expression = ParseArithmeticExpression2();

            while (Match(TokenType.OpPlus, TokenType.OpMinus))
            {
                if (Accept(TokenType.OpPlus))
                    expression = new AdditionExpression(expression, ParseArithmeticExpression2());
                else if (Accept(TokenType.OpMinus))
                    expression = new SubtractionExpression(expression, ParseArithmeticExpression2());
            }

            return expression;
        }

        private Expression ParseArithmeticExpression2()
        {
            var expression = ParseUnaryExpression();

            while (Match(TokenType.OpMul, TokenType.OpDiv, TokenType.OpMod))
            {
                if (Accept(TokenType.OpMul))
                    expression = new MultiplicationExpression(expression, ParseUnaryExpression());
                else if (Accept(TokenType.OpDiv))
                    expression = new DivisionExpression(expression, ParseUnaryExpression());
                else if (Accept(TokenType.OpMod))
                    expression = new ModulusExpression(expression, ParseUnaryExpression());
            }

            return expression;
        }

        private Expression ParseUnaryExpression()
        {
            if (Accept(TokenType.OpExclamation))
                return new UnaryNotExpression(ParseUnaryExpression());
            else if (Accept(TokenType.OpMinus))
                return new UnaryMinusExpression(ParseUnaryExpression());

            return ParsePrimaryExpression();
        }

        private Expression ParsePrimaryExpression()
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

            throw new ParserException(Current(), "Expected expression");
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

        private bool Match(params TokenType[] types)
        {
            var current = Current();

            foreach (var type in types)
            {
                if (current.TokenType == type)
                    return true;
            }

            return false;
        }

        private Token Current()
        {
            return _tokenStream.Current;
        }

        private void Advance()
        {
            _tokenStream.MoveNext();
        }
    }
}