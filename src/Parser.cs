using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Errors;
using TmLox.Ast.Statements;
using TmLox.Ast.Expressions;
using TmLox.Ast.Expressions.Unary;
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

            while (!Accept(Lexeme.Eof))
            {
                statements.Add(ParseStatement());
            }

            return statements;
        }

        private Statement ParseStatement()
        {
            if (Accept(Lexeme.KwVar))
                return ParseVariableDeclaration();
            else if (Accept(Lexeme.KwIf))
                return ParseIf();
            else if (Accept(Lexeme.KwFun))
                return ParseFunctionDeclaration();
            else if (Accept(Lexeme.KwReturn))
                return ParseReturn();
            else if (Accept(Lexeme.KwBreak))
                return ParseBreak();
            else if (Accept(Lexeme.KwFor))
                return ParseFor();
            else if (Accept(Lexeme.KwWhile))
                return ParseWhile();

            return ParseExpressionStatement();
        }

        private VariableDeclarationStatement ParseVariableDeclaration()
        {
            var name = Expect(Lexeme.Identifier, "Identifier");
            Expression? value = null;

            if (Accept(Lexeme.OpAssign))
                value = ParseExpression();

            Expect(Lexeme.OpSemicolon, ";");
            return new VariableDeclarationStatement(name, value);
        }

        private IfStatement ParseIf()
        {
            Expect(Lexeme.OPLParen, "(");
            var condition = ParseExpression();
            Expect(Lexeme.OpRParen, ")");

            Expect(Lexeme.OpLBrace, "{");
            var body = ParseBlock();
            Expect(Lexeme.OPRBrace, "}");

            var elseIfStatements = new List<ElseIfStatement>();

            while (Accept(Lexeme.KwElif))
            {
                Expect(Lexeme.OPLParen, "(");
                var elseIfCondition = ParseExpression();
                Expect(Lexeme.OpRParen, ")");

                Expect(Lexeme.OpLBrace, "{");
                var elseIfBody = ParseBlock();
                Expect(Lexeme.OPRBrace, "}");

                elseIfStatements.Add(new ElseIfStatement(elseIfCondition, elseIfBody));
            }

            IList<Statement>? elseBody = null;

            if (Accept(Lexeme.KwElse))
            {
                Expect(Lexeme.OpLBrace, "{");
                elseBody = ParseBlock();
                Expect(Lexeme.OPRBrace, "}");
            }

            return new IfStatement(condition, body, elseIfStatements, elseBody);
        }

        private FunctionDeclarationStatement ParseFunctionDeclaration()
        {
            var name = Expect(Lexeme.Identifier, "Identifier");

            Expect(Lexeme.OPLParen, "(");
            var parameters = ParseFunctionParameters();
            Expect(Lexeme.OpRParen, ")");

            Expect(Lexeme.OpLBrace, "{");
            var body = ParseBlock();
            Expect(Lexeme.OPRBrace, "}");

            return new FunctionDeclarationStatement(name, parameters, body);
        }

        private IList<string> ParseFunctionParameters()
        {
            var parameters = new List<string>();

            while (!Match(Lexeme.OpRParen))
            {
                var parameter = Expect(Lexeme.Identifier, "Identifier");
                parameters.Add(parameter.Value.AsString());

                if (!Match(Lexeme.OpRParen))
                    Expect(Lexeme.OpComma, ",");
            }

            return parameters;
        }

        private ReturnStatement ParseReturn()
        {
            Expression? value = null;

            if (!Match(Lexeme.OpSemicolon))
                value = ParseExpression();

            Expect(Lexeme.OpSemicolon, ";");
            return new ReturnStatement(value);
        }

        private ForStatement ParseFor()
        {
            Expect(Lexeme.OPLParen, "(");

            Statement? initial = null;
            if (!Accept(Lexeme.OpSemicolon))
            {
                if (Accept(Lexeme.KwVar))
                    initial = ParseVariableDeclaration();
                else
                    initial = ParseExpressionStatement();
            }

            Expression? condition = null;
            if (!Accept(Lexeme.OpSemicolon))
            {
                condition = ParseExpression();
                Expect(Lexeme.OpSemicolon, ";");
            }

            Expression? increment = null;
            if (!Accept(Lexeme.OpRParen))
            {
                increment = ParseExpression();
                Expect(Lexeme.OpRParen, ")");
            }


            Expect(Lexeme.OpLBrace, "{");
            var body = ParseBlock();
            Expect(Lexeme.OPRBrace, "}");

            return new ForStatement(initial, condition, increment, body);
        }

        private WhileStatement ParseWhile()
        {
            Expect(Lexeme.OPLParen, "(");
            var condition = ParseExpression();
            Expect(Lexeme.OpRParen, ")");

            Expect(Lexeme.OpLBrace, "{");
            var body = ParseBlock();
            Expect(Lexeme.OPRBrace, "}");

            return new WhileStatement(condition, body);
        }

        private BreakStatement ParseBreak()
        {
            Expect(Lexeme.OpSemicolon, ";");

            return new BreakStatement();
        }

        private IList<Statement> ParseBlock()
        {
            var statements = new List<Statement>();

            while (!Match(Lexeme.OPRBrace))
            {
                statements.Add(ParseStatement());
            }

            return statements;
        }

        private Statement ParseExpressionStatement()
        {
            var expression = ParseExpression();
            Expect(Lexeme.OpSemicolon, ";");

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

            while (Accept(Lexeme.KwOr))
                expression = new OrExpression(expression, ParseAndExpression());

            return expression;
        }

        private Expression ParseAndExpression()
        {
            var expression = ParseEqualityExpression();

            while (Accept(Lexeme.KwAnd))
                expression = new AndExpression(expression, ParseEqualityExpression());

            return expression;
        }

        private Expression ParseEqualityExpression()
        {
            var expression = ParseComparisonExpression();

            while (Match(Lexeme.OpEq, Lexeme.OpNotEqual))
            {
                if (Accept(Lexeme.OpEq))
                    expression = new EqualExpression(expression, ParseComparisonExpression());
                else if (Accept(Lexeme.OpNotEqual))
                    expression = new NotEqualExpression(expression, ParseComparisonExpression());
            }

            return expression;
        }

        private Expression ParseComparisonExpression()
        {
            var expression = ParseArithmeticExpression1();

            if (Accept(Lexeme.OpLess))
                return new LessExpression(expression, ParseArithmeticExpression1());
            else if (Accept(Lexeme.OpLessEq))
                return new LessEqualExpression(expression, ParseArithmeticExpression1());
            else if (Accept(Lexeme.OpMore))
                return new MoreExpression(expression, ParseArithmeticExpression1());
            else if (Accept(Lexeme.OpMoreEq))
                return new MoreEqualExpression(expression, ParseArithmeticExpression1());

            return expression;
        }

        private Expression ParseArithmeticExpression1()
        {
            var expression = ParseArithmeticExpression2();

            while (Match(Lexeme.OpPlus, Lexeme.OpMinus))
            {
                if (Accept(Lexeme.OpPlus))
                    expression = new AdditionExpression(expression, ParseArithmeticExpression2());
                else if (Accept(Lexeme.OpMinus))
                    expression = new SubtractionExpression(expression, ParseArithmeticExpression2());
            }

            return expression;
        }

        private Expression ParseArithmeticExpression2()
        {
            var expression = ParseUnaryExpression();

            while (Match(Lexeme.OpMul, Lexeme.OpDiv, Lexeme.OpMod))
            {
                if (Accept(Lexeme.OpMul))
                    expression = new MultiplicationExpression(expression, ParseUnaryExpression());
                else if (Accept(Lexeme.OpDiv))
                    expression = new DivisionExpression(expression, ParseUnaryExpression());
                else if (Accept(Lexeme.OpMod))
                    expression = new ModulusExpression(expression, ParseUnaryExpression());
            }

            return expression;
        }

        private Expression ParseUnaryExpression()
        {
            if (Accept(Lexeme.OpExclamation))
                return new UnaryNotExpression(ParseUnaryExpression());
            else if (Accept(Lexeme.OpMinus))
                return new UnaryMinusExpression(ParseUnaryExpression());

            return ParsePrimaryExpression();
        }

        private Expression ParsePrimaryExpression()
        {
            if (Accept(Lexeme.OPLParen))
            {
                var expression = ParseExpression();
                Expect(Lexeme.OpRParen, ")");

                return expression;
            }

            if (Accept(Lexeme.KwNil))
                return new LiteralExpression(AnyValue.CreateNull());
            else if (Accept(Lexeme.KwFalse))
                return new LiteralExpression(AnyValue.CreateBool(false));
            else if (Accept(Lexeme.KwTrue))
                return new LiteralExpression(AnyValue.CreateBool(true));
            else if (Accept(Lexeme.LitInt, out var intToken))
                return new LiteralExpression(intToken.Value);
            else if (Accept(Lexeme.LitFloat, out var floatToken))
                return new LiteralExpression(floatToken.Value);
            else if (Accept(Lexeme.LitString, out var stringToken))
                return new LiteralExpression(stringToken.Value);
            else if (Accept(Lexeme.Identifier, out var identifierToken))
            {
                Expression expression = new VariableExpression(identifierToken);

                if (Accept(Lexeme.OpAssign))
                    expression = new VariableAssigmentExpression(identifierToken, ParseExpression());
                else if (Accept(Lexeme.OpPlusEq))
                    expression = new VariableAdditionExpression(identifierToken, ParseExpression());
                else if (Accept(Lexeme.OpMinusEq))
                    expression = new VariableSubtractionExpression(identifierToken, ParseExpression());
                else if (Accept(Lexeme.OpMulEq))
                    expression = new VariableMultiplicationExpression(identifierToken, ParseExpression());
                else if (Accept(Lexeme.OpDivEq))
                    expression = new VariableDivisionExpression(identifierToken, ParseExpression());
                else if (Accept(Lexeme.OpModEq))
                    expression = new VariableModulusExpression(identifierToken, ParseExpression());
                else if (Accept(Lexeme.OPLParen))
                    expression = new FunctionCallExpression(identifierToken, ParseFunctionArguments());

                return expression;
            }

            // some errors
            if (Accept(Lexeme.KwElif))
                throw new SyntaxError(Current(), "\"elif\" used without coresponding \"if\"");
            else if (Accept(Lexeme.KwElse))
                throw new SyntaxError(Current(), "\"else\" used without coresponding \"if\"");


            throw new SyntaxError(Current(), "Invalid expression");
        }

        private IList<Expression> ParseFunctionArguments()
        {
            var arguments = new List<Expression>();

            while (!Accept(Lexeme.OpRParen))
            {
                arguments.Add(ParseExpression());

                if (!Match(Lexeme.OpRParen))
                    Expect(Lexeme.OpComma, ",");
            }

            return arguments;
        }

        private bool Accept(Lexeme tokenType, out Token value)
        {
            var current = Current();

            if (current.Lexeme == tokenType)
            {
                Advance();
                value = current;
                return true;
            }

            value = default;
            return false;
        }

        private bool Accept(Lexeme tokenType)
        {
            return Accept(tokenType, out var _);
        }

        private Token Expect(Lexeme tokenType, string expected)
        {
            return Accept(tokenType, out var token) ? token : throw new SyntaxError(Current(), $"Expected: \"{expected}\"");
        }

        private bool Match(params Lexeme[] types)
        {
            var current = Current();

            foreach (var type in types)
            {
                if (current.Lexeme == type)
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