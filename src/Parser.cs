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
    public class Parser
    {
        private readonly IEnumerator<Token> _tokenStream;

        public Parser(IEnumerator<Token> tokenStream)
        {
            _tokenStream = tokenStream;
        }

        public IList<Statement> Parse()
        {
            var statements = new List<Statement>();

            while (!Accept(TokenType.Eof))
            {
                statements.Add(ParseStatement());
            }

            return statements;
        }

        private Statement ParseStatement()
        {
            if (Accept(TokenType.KwVar))
                return ParseVariableDeclaration();
            else if (Accept(TokenType.KwIf))
                return ParseIf();
            else if (Accept(TokenType.KwFun))
                return ParseFunctionDeclaration();
            else if (Accept(TokenType.KwReturn))
                return ParseReturn();
            else if (Accept(TokenType.KwBreak))
                return ParseBreak();
            else if (Accept(TokenType.KwFor))
                return ParseFor();
            else if (Accept(TokenType.KwWhile))
                return ParseWhile();

            return ParseExpressionStatement();
        }

        private VariableDeclarationStatement ParseVariableDeclaration()
        {
            var name = Expect(TokenType.Identifier, "Identifier");
            Expression? value = null;

            if (Accept(TokenType.OpAssign))
                value = ParseExpression();

            Expect(TokenType.OpSemicolon, ";");
            return new VariableDeclarationStatement(name, value);
        }

        private IfStatement ParseIf()
        {
            Expect(TokenType.OPLParen, "(");
            var condition = ParseExpression();
            Expect(TokenType.OpRParen, ")");

            Expect(TokenType.OpLBrace, "{");
            var body = ParseBlock();
            Expect(TokenType.OPRBrace, "}");

            var elseIfStatements = new List<ElseIfStatement>();

            while (Accept(TokenType.KwElif))
            {
                Expect(TokenType.OPLParen, "(");
                var elseIfCondition = ParseExpression();
                Expect(TokenType.OpRParen, ")");

                Expect(TokenType.OpLBrace, "{");
                var elseIfBody = ParseBlock();
                Expect(TokenType.OPRBrace, "}");

                elseIfStatements.Add(new ElseIfStatement(elseIfCondition, elseIfBody));
            }

            IList<Statement>? elseBody = null;

            if (Accept(TokenType.KwElse))
            {
                Expect(TokenType.OpLBrace, "{");
                elseBody = ParseBlock();
                Expect(TokenType.OPRBrace, "}");
            }

            return new IfStatement(condition, body, elseIfStatements, elseBody);
        }

        private FunctionDeclarationStatement ParseFunctionDeclaration()
        {
            var name = Expect(TokenType.Identifier, "Identifier");

            Expect(TokenType.OPLParen, "(");
            var parameters = ParseFunctionParameters();
            Expect(TokenType.OpRParen, ")");

            Expect(TokenType.OpLBrace, "{");
            var body = ParseBlock();
            Expect(TokenType.OPRBrace, "}");

            return new FunctionDeclarationStatement(name, parameters, body);
        }

        private IList<string> ParseFunctionParameters()
        {
            var parameters = new List<string>();

            while (!Match(TokenType.OpRParen))
            {
                var parameter = Expect(TokenType.Identifier, "Identifier");
                parameters.Add(parameter.Value as string);

                if (!Match(TokenType.OpRParen))
                    Expect(TokenType.OpComma, ",");
            }

            return parameters;
        }

        private ReturnStatement ParseReturn()
        {
            Expression? value = null;

            if (!Match(TokenType.OpSemicolon))
                value = ParseExpression();

            Expect(TokenType.OpSemicolon, ";");
            return new ReturnStatement(value);
        }

        private ForStatement ParseFor()
        {
            Expect(TokenType.OPLParen, "(");

            Statement? initial = null;
            if (!Accept(TokenType.OpSemicolon))
            {
                if (Accept(TokenType.KwVar))
                    initial = ParseVariableDeclaration();
                else
                    initial = ParseExpressionStatement();
            }

            Expression? condition = null;
            if (!Accept(TokenType.OpSemicolon))
            {
                condition = ParseExpression();
                Expect(TokenType.OpSemicolon, ";");
            }

            Expression? increment = null;
            if (!Accept(TokenType.OpRParen))
            {
                increment = ParseExpression();
                Expect(TokenType.OpRParen, ")");
            }


            Expect(TokenType.OpLBrace, "{");
            var body = ParseBlock();
            Expect(TokenType.OPRBrace, "}");

            return new ForStatement(initial, condition, increment, body);
        }

        private WhileStatement ParseWhile()
        {
            Expect(TokenType.OPLParen, "(");
            var condition = ParseExpression();
            Expect(TokenType.OpRParen, ")");

            Expect(TokenType.OpLBrace, "{");
            var body = ParseBlock();
            Expect(TokenType.OPRBrace, "}");

            return new WhileStatement(condition, body);
        }

        private BreakStatement ParseBreak()
        {
            Expect(TokenType.OpSemicolon, ";");

            return new BreakStatement();
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

        private Statement ParseExpressionStatement()
        {
            var expression = ParseExpression();
            Expect(TokenType.OpSemicolon, ";");

            return expression;
        }

        private Expression ParseExpression()
        {
            var expression = ParseOrExpression();
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

            while (Match(TokenType.OpEq, TokenType.OpNotEqual))
            {
                if (Accept(TokenType.OpEq))
                    expression = new EqualExpression(expression, ParseComparisonExpression());
                else if (Accept(TokenType.OpNotEqual))
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
            if (Accept(TokenType.OPLParen))
            {
                var expression = ParseExpression();
                Expect(TokenType.OpRParen, ")");

                return expression;
            }

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
            {
                Expression expression = new VariableExpression(identifierToken);

                if (Accept(TokenType.OpAssign))
                    expression = new VariableAssigmentExpression(identifierToken, ParseExpression());
                else if (Accept(TokenType.OpPlusEq))
                    expression = new VariableAdditionExpression(identifierToken, ParseExpression());
                else if (Accept(TokenType.OpMinusEq))
                    expression = new VariableSubtractionExpression(identifierToken, ParseExpression());
                else if (Accept(TokenType.OpMulEq))
                    expression = new VariableMultiplicationExpression(identifierToken, ParseExpression());
                else if (Accept(TokenType.OpDivEq))
                    expression = new VariableDivisionExpression(identifierToken, ParseExpression());
                else if (Accept(TokenType.OpModEq))
                    expression = new VariableModulusExpression(identifierToken, ParseExpression());
                else if (Accept(TokenType.OPLParen))
                    expression = new FunctionCallExpression(identifierToken, ParseFunctionArguments());

                return expression;
            }

            // some errors
            if (Accept(TokenType.KwElif))
                throw new SyntaxError(Current(), "\"elif\" used without coresponding \"if\"");
            else if (Accept(TokenType.KwElse))
                throw new SyntaxError(Current(), "\"else\" used without coresponding \"if\"");


            throw new SyntaxError(Current(), "Invalid expression");
        }

        private IList<Expression> ParseFunctionArguments()
        {
            var arguments = new List<Expression>();

            while (!Accept(TokenType.OpRParen))
            {
                arguments.Add(ParseExpression());

                if (!Match(TokenType.OpRParen))
                    Expect(TokenType.OpComma, ",");
            }

            return arguments;
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
            return Accept(tokenType, out var token) ? token : throw new SyntaxError(Current(), $"Expected: \"{expected}\"");
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