using System;
using System.Linq;
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
    public class TreeWalkingInterpreter : IVisitor<AnyValue>
    {
        private Dictionary<string, AnyValue> _variables;

        public TreeWalkingInterpreter()
        {
            _variables = new Dictionary<string, AnyValue>();

            var print = AnyValue.FromCallable(new PrintFunction());
            _variables.Add("print", print);
        }

        public void Interpret(IList<Statement> statements)
        {
            foreach (var statement in statements)
            {
                Execute(statement);
            }
        }

        private AnyValue Execute(Statement statement)
        {
            if (statement != null)
                return statement.Accept(this);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(BreakStatement breakStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(ForStatement forStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(FunctionDeclarationStatement functionDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(IfStatement ifStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(ElseIfStatement elseIfStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(ReturnStatement returnStatement)
        {
            if (returnStatement.Value != null)
                return Execute(returnStatement.Value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableDeclarationStatement variableDeclarationStatement)
        {
            var value = Execute(variableDeclarationStatement.Value);
            _variables.Add(variableDeclarationStatement.Name, value);

            return value;
        }

        public AnyValue Visit(WhileStatement whileStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(AdditionExpression additionExpression)
        {
            var lhs = Execute(additionExpression.Left);
            var rhs = Execute(additionExpression.Right);

            return Add(lhs, rhs);
        }

        public AnyValue Visit(DivisionExpression divisionExpression)
        {
            var lhs = Execute(divisionExpression.Left);
            var rhs = Execute(divisionExpression.Right);

            return Divide(lhs, rhs);
        }

        public AnyValue Visit(ModulusExpression modulusExpression)
        {
            var lhs = Execute(modulusExpression.Left);
            var rhs = Execute(modulusExpression.Right);

            return Modulus(lhs, rhs);
        }

        public AnyValue Visit(MultiplicationExpression multiplicationExpression)
        {
            var lhs = Execute(multiplicationExpression.Left);
            var rhs = Execute(multiplicationExpression.Right);

            return Multiply(lhs, rhs);
        }

        public AnyValue Visit(SubtractionExpression subtractionExpression)
        {
            var lhs = Execute(subtractionExpression.Left);
            var rhs = Execute(subtractionExpression.Right);

            return Subtract(lhs, rhs);
        }

        public AnyValue Visit(AndExpression andExpression)
        {
            var lhs = CheckBool(Execute(andExpression.Left));

            if (lhs)
            {
                return AnyValue.FromBool(CheckBool(Execute(andExpression.Right)));
            }

            return AnyValue.FromBool(false);
        }

        public AnyValue Visit(EqualExpression equalExpression)
        {
            var lhs = Execute(equalExpression.Left);
            var rhs = Execute(equalExpression.Right);

            return AnyValue.FromBool(Equals(lhs.Value, rhs.Value));
        }

        public AnyValue Visit(LessEqualExpression lessEqualExpression)
        {
            var lhs = Execute(lessEqualExpression.Left);
            var rhs = Execute(lessEqualExpression.Right);

            return AnyValue.FromBool(!IsMore(lhs, rhs));
        }

        public AnyValue Visit(LessExpression lessExpression)
        {
            var lhs = Execute(lessExpression.Left);
            var rhs = Execute(lessExpression.Right);

            return AnyValue.FromBool(IsLess(lhs, rhs));
        }

        public AnyValue Visit(MoreEqualExpression moreEqualExpression)
        {
            var lhs = Execute(moreEqualExpression.Left);
            var rhs = Execute(moreEqualExpression.Right);

            return AnyValue.FromBool(!IsLess(lhs, rhs));
        }

        public AnyValue Visit(MoreExpression moreExpression)
        {
            var lhs = Execute(moreExpression.Left);
            var rhs = Execute(moreExpression.Right);

            return AnyValue.FromBool(IsMore(lhs, rhs));
        }

        public AnyValue Visit(NotEqualExpression notEqualExpression)
        {
            var lhs = Execute(notEqualExpression.Left);
            var rhs = Execute(notEqualExpression.Right);

            return AnyValue.FromBool(!Equals(lhs.Value, rhs.Value));
        }

        public AnyValue Visit(OrExpression orExpression)
        {
            var lhs = CheckBool(Execute(orExpression.Left));

            if (!lhs)
            {
                return AnyValue.FromBool(CheckBool(Execute(orExpression.Right)));
            }

            return AnyValue.FromBool(true);
        }

        public AnyValue Visit(BoolLiteralExpression boolLiteralExpression)
        {
            return AnyValue.FromBool(boolLiteralExpression.Value);
        }

        public AnyValue Visit(FloatLiteralExpression floatLiteralExpression)
        {
            return AnyValue.FromFloat(floatLiteralExpression.Value);
        }

        public AnyValue Visit(IntLiteralExpression intLiteralExpression)
        {
            return AnyValue.FromInteger(intLiteralExpression.Value);
        }

        public AnyValue Visit(NullLiteralExpression nullLiteralExpression)
        {
            return AnyValue.FromNull();
        }

        public AnyValue Visit(StringLiteralExpression stringLiteralExpression)
        {
            return AnyValue.FromString(stringLiteralExpression.Value);
        }

        public AnyValue Visit(UnaryMinusExpression unaryMinusExpression)
        {
            var value = Execute(unaryMinusExpression.Expression);

            return value.Type switch
            {
                ValueType.Integer => AnyValue.FromInteger(-value.AsInteger()),
                ValueType.Float => AnyValue.FromFloat(-value.AsFloat()),
                _ => throw new ValueError($"Operator - not supported for {value.Type}")
            };
        }

        public AnyValue Visit(UnaryNotExpression unaryNotExpression)
        {
            var value = Execute(unaryNotExpression.Expression);

            return value.Type switch
            {
                ValueType.Bool => AnyValue.FromBool(!value.AsBool()),
                _ => throw new ValueError($"Operator ! not supported for {value.Type}")
            };
        }

        public AnyValue Visit(VariableAdditionExpression variableAdditionExpression)
        {
            var variable = GetVariable(variableAdditionExpression.Variable);
            var value = Execute(variableAdditionExpression.Value);

            _variables[variableAdditionExpression.Variable] = Add(variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableAssigmentExpression variableAssigmentExpression)
        {
            GetVariable(variableAssigmentExpression.Variable);
            var value = Execute(variableAssigmentExpression.Value);

            _variables[variableAssigmentExpression.Variable] = value;

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableDivisionExpression variableDivisionExpression)
        {
            var variable = GetVariable(variableDivisionExpression.Variable);
            var value = Execute(variableDivisionExpression.Value);

            _variables[variableDivisionExpression.Variable] = Divide(variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableModulusExpression variableModulusExpression)
        {
            var variable = GetVariable(variableModulusExpression.Variable);
            var value = Execute(variableModulusExpression.Value);

            _variables[variableModulusExpression.Variable] = Modulus(variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableMultiplicationExpression variableMultiplicationExpression)
        {
            var variable = GetVariable(variableMultiplicationExpression.Variable);
            var value = Execute(variableMultiplicationExpression.Value);

            _variables[variableMultiplicationExpression.Variable] = Multiply(variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableSubtractionExpression variableSubtractionExpression)
        {
            var variable = GetVariable(variableSubtractionExpression.Variable);
            var value = Execute(variableSubtractionExpression.Value);

            _variables[variableSubtractionExpression.Variable] = Subtract(variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(FunctionCallExpression functionCallExpression)
        {
            var function = GetFunction(functionCallExpression.Name);
            var arguments = functionCallExpression.Arguments.Select(e => Execute(e));

            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                return function.Call(arguments);
            }
            finally
            {
                _variables = currentVariables;
            }
        }

        public AnyValue Visit(VariableExpression variableExpression)
        {
            return GetVariable(variableExpression.Name);
        }

        private AnyValue GetVariable(string name)
        {
            if (_variables.TryGetValue(name, out var variable))
            {
                if (!variable.IsPrimitive())
                    throw new ValueError($"Cannot perform operation with {name} which is instance of {variable.Type}");

                return variable;
            }

            throw new ValueError($"Variable {name} does not exist");
        }

        private IFunction GetFunction(string name)
        {
            if (_variables.TryGetValue(name, out var variable))
            {
                if (!variable.IsFunction())
                    throw new ValueError($"{name} is instance of {variable.Type} and not a function");

                return variable.AsFunction();
            }

            throw new ValueError($"Function {name} does not exist");
        }

        private static AnyValue Add(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.FromInteger(lhs.AsInteger() + rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.FromFloat(lhs.AsFloat() + rhs.AsFloat());

            throw new ValueError($"Operator + not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Subtract(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.FromInteger(lhs.AsInteger() - rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.FromFloat(lhs.AsFloat() - rhs.AsFloat());

            throw new ValueError($"Operator - not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Multiply(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.FromInteger(lhs.AsInteger() * rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.FromFloat(lhs.AsFloat() * rhs.AsFloat());

            throw new ValueError($"Operator * not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Divide(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.FromFloat(lhs.AsFloat() / rhs.AsFloat());

            throw new ValueError($"Operator / not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Modulus(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.FromInteger(lhs.AsInteger() % rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.FromFloat(lhs.AsFloat() % rhs.AsFloat());

            throw new ValueError($"Operator % not supported for {lhs.Type} and {rhs.Type}");
        }

        private static bool IsLess(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsNumber() && rhs.IsNumber())
                return lhs.AsFloat() < rhs.AsFloat();

            throw new ValueError($"Cannot compare {lhs.Value} and {rhs.Value}");
        }

        private static bool IsMore(AnyValue lhs, AnyValue rhs)
        {
            return IsLess(rhs, lhs);
        }

        private static bool CheckBool(AnyValue value)
        {
            if (!value.IsBool())
                throw new ValueError($"Cannot convert {value.Type} to bool");

            return value.AsBool();
        }
    }
}
