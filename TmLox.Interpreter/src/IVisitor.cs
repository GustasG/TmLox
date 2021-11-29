using TmLox.Interpreter.Ast.Statements;
using TmLox.Interpreter.Ast.Expressions;
using TmLox.Interpreter.Ast.Expressions.Unary;
using TmLox.Interpreter.Ast.Expressions.Variable;
using TmLox.Interpreter.Ast.Expressions.Binary.Logical;
using TmLox.Interpreter.Ast.Expressions.Binary.Arithmetic;

namespace TmLox.Interpreter
{
    public interface IVisitor<T>
    {
        // Statements
        T Visit(BreakStatement breakStatement);

        T Visit(ForStatement forStatement);

        T Visit(FunctionDeclarationStatement functionDeclarationStatement);

        T Visit(IfStatement ifStatement);

        T Visit(ElseIfStatement elseIfStatement);

        T Visit(ReturnStatement returnStatement);

        T Visit(VariableDeclarationStatement variableDeclarationStatement);

        T Visit(WhileStatement whileStatement);


        // Expressions
        T Visit(AdditionExpression additionExpression);

        T Visit(DivisionExpression divisionExpression);

        T Visit(ModulusExpression modulusExpression);

        T Visit(MultiplicationExpression multiplicationExpression);

        T Visit(SubtractionExpression subtractionExpression);

        T Visit(AndExpression andExpression);

        T Visit(EqualExpression equalExpression);

        T Visit(LessEqualExpression lessEqualExpression);

        T Visit(LessExpression lessExpression);

        T Visit(MoreEqualExpression moreEqualExpression);

        T Visit(MoreExpression moreExpression);

        T Visit(NotEqualExpression notEqualExpression);

        T Visit(OrExpression orExpression);

        T Visit(LiteralExpression literalExpression);

        T Visit(UnaryMinusExpression unaryMinusExpression);

        T Visit(UnaryNotExpression unaryNotExpression);

        T Visit(VariableAdditionExpression variableAdditionExpression);

        T Visit(VariableAssigmentExpression variableAssigmentExpression);

        T Visit(VariableDivisionExpression variableDivisionExpression);

        T Visit(VariableModulusExpression variableModulusExpression);

        T Visit(VariableMultiplicationExpression variableMultiplicationExpression);

        T Visit(VariableSubtractionExpression variableSubtractionExpression);

        T Visit(FunctionCallExpression functionCallExpression);

        T Visit(VariableExpression variableExpression);
    }
}