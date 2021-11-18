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
using TmLox.Interpreter.StackUnwinding;
using TmLox.Ast.Expressions.Binary.Logical;
using TmLox.Ast.Expressions.Binary.Arithmetic;

namespace TmLox.Interpreter
{
    public class TreeWalkingInterpreter : IInterpreter
    {
        private Dictionary<string, AnyValue> _variables;

        public TreeWalkingInterpreter()
        {
            _variables = new Dictionary<string, AnyValue>();
        }

        public void Execute(IList<Statement> statements)
        {
            foreach (var statement in statements)
                Execute(statement);
        }

        public AnyValue Execute(Statement statement)
        {
            if (statement != null)
                return statement.Accept(this);

            return AnyValue.FromNull();
        }

        public void AddVariable(string name, AnyValue value)
        {
            _variables[name] = value;
        }

        public AnyValue Visit(BreakStatement breakStatement)
        {
            throw new BreakUnwind();
        }

        public AnyValue Visit(ForStatement forStatement)
        {
            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                if (forStatement.Initial != null)
                    Execute(forStatement.Initial);

                while (CheckBool(Execute(forStatement.Condition)))
                {
                    Execute(forStatement.Body);
                    Execute(forStatement.Increment);
                }

            }
            catch(BreakUnwind)
            {

            }
            finally
            {
                _variables = currentVariables;
            }

            return AnyValue.FromNull();
        }

        public AnyValue Visit(FunctionDeclarationStatement functionDeclarationStatement)
        {
            var function = new LoxFunction(functionDeclarationStatement.Parameters, functionDeclarationStatement.Body);
            AddVariable(functionDeclarationStatement.Name, AnyValue.FromFunction(function));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(IfStatement ifStatement)
        {
            // TODO: Limit variable scope for appropriate if, elif, else blocks
            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                bool checkElse = true;

                if (CheckBool(Execute(ifStatement.Condition)))
                {
                    Execute(ifStatement.Body);
                    checkElse = false;
                }
                else if(ifStatement.ElseIfStatements != null)
                {
                    foreach (var elifStatement in ifStatement.ElseIfStatements)
                    {
                        if (CheckBool(Execute(elifStatement.Condition)))
                        {
                            Execute(elifStatement.Body);
                            checkElse = false;
                            break;
                        }
                    }
                }

                if (checkElse && ifStatement.ElseBody != null)
                    Execute(ifStatement.ElseBody);
            }
            finally
            {
                _variables = currentVariables;
            }

            return AnyValue.FromNull();
        }

        public AnyValue Visit(ElseIfStatement elseIfStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(ReturnStatement returnStatement)
        {
            var value = AnyValue.FromNull();

            if (returnStatement.Value != null)
                value = Execute(returnStatement.Value);

            throw new ReturnUnwind(value);
        }

        public AnyValue Visit(VariableDeclarationStatement variableDeclarationStatement)
        {
            var value = Execute(variableDeclarationStatement.Value);
            AddVariable(variableDeclarationStatement.Name, value);

            return value;
        }

        public AnyValue Visit(WhileStatement whileStatement)
        {
            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                while (CheckBool(Execute(whileStatement.Condition)))
                    Execute(whileStatement.Body);
            }
            catch (BreakUnwind)
            {

            }
            finally
            {
                _variables = currentVariables;
            }

            return AnyValue.FromNull();
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
                return AnyValue.FromBool(CheckBool(Execute(andExpression.Right)));

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
                return AnyValue.FromBool(CheckBool(Execute(orExpression.Right)));

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
                AnyValueType.Integer => AnyValue.FromInteger(-value.AsInteger()),
                AnyValueType.Float => AnyValue.FromFloat(-value.AsFloat()),
                _ => throw new ValueError($"Operator - not supported for {value.Type}")
            };
        }

        public AnyValue Visit(UnaryNotExpression unaryNotExpression)
        {
            var value = Execute(unaryNotExpression.Expression);

            return value.Type switch
            {
                AnyValueType.Bool => AnyValue.FromBool(!value.AsBool()),
                _ => throw new ValueError($"Operator ! not supported for {value.Type}")
            };
        }

        public AnyValue Visit(VariableAdditionExpression variableAdditionExpression)
        {
            var variable = GetVariable(variableAdditionExpression.Variable);
            var value = Execute(variableAdditionExpression.Value);

            AddVariable(variableAdditionExpression.Variable, Add(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableAssigmentExpression variableAssigmentExpression)
        {
            GetVariable(variableAssigmentExpression.Variable);
            var value = Execute(variableAssigmentExpression.Value);

            AddVariable(variableAssigmentExpression.Variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableDivisionExpression variableDivisionExpression)
        {
            var variable = GetVariable(variableDivisionExpression.Variable);
            var value = Execute(variableDivisionExpression.Value);

            AddVariable(variableDivisionExpression.Variable, Divide(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableModulusExpression variableModulusExpression)
        {
            var variable = GetVariable(variableModulusExpression.Variable);
            var value = Execute(variableModulusExpression.Value);

            AddVariable(variableModulusExpression.Variable, Modulus(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableMultiplicationExpression variableMultiplicationExpression)
        {
            var variable = GetVariable(variableMultiplicationExpression.Variable);
            var value = Execute(variableMultiplicationExpression.Value);

            AddVariable(variableMultiplicationExpression.Variable, Multiply(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableSubtractionExpression variableSubtractionExpression)
        {
            var variable = GetVariable(variableSubtractionExpression.Variable);
            var value = Execute(variableSubtractionExpression.Value);

            AddVariable(variableSubtractionExpression.Variable, Subtract(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(FunctionCallExpression functionCallExpression)
        {
            var function = GetFunction(functionCallExpression.Name);

            var arguments = functionCallExpression.Arguments.
                Select(e => Execute(e))
                .ToList();

            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                return function.Call(this, arguments);
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
