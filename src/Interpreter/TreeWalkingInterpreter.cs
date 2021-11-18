using System;
using System.Linq;
using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Errors;
using TmLox.Functions;
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

        public void Execute(Statement statement)
        {
            statement.Accept(this);
        }

        public AnyValue Evaluate(Statement statement)
        {
            return statement.Accept(this);
        }

        public void RegisterVariable(string name, AnyValue value)
        {
            _variables[name] = value;
        }

        public void RegisterFunction(string name, IFunction function)
        {
            var fun = AnyValue.FromFunction(function);
            RegisterVariable(name, fun);
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

                while (CheckBool(Evaluate(forStatement.Condition)))
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
            RegisterFunction(functionDeclarationStatement.Name, function);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(IfStatement ifStatement)
        {
            // TODO: Limit variable scope for appropriate if, elif, else blocks
            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                bool checkElse = true;

                if (CheckBool(Evaluate(ifStatement.Condition)))
                {
                    Execute(ifStatement.Body);
                    checkElse = false;
                }
                else if(ifStatement.ElseIfStatements != null)
                {
                    foreach (var elifStatement in ifStatement.ElseIfStatements)
                    {
                        if (CheckBool(Evaluate(elifStatement.Condition)))
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
                value = Evaluate(returnStatement.Value);

            throw new ReturnUnwind(value);
        }

        public AnyValue Visit(VariableDeclarationStatement variableDeclarationStatement)
        {
            var value = Evaluate(variableDeclarationStatement.Value);
            RegisterVariable(variableDeclarationStatement.Name, value);

            return value;
        }

        public AnyValue Visit(WhileStatement whileStatement)
        {
            var currentVariables = new Dictionary<string, AnyValue>(_variables);

            try
            {
                while (CheckBool(Evaluate(whileStatement.Condition)))
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
            var lhs = Evaluate(additionExpression.Left);
            var rhs = Evaluate(additionExpression.Right);

            return Add(lhs, rhs);
        }

        public AnyValue Visit(DivisionExpression divisionExpression)
        {
            var lhs = Evaluate(divisionExpression.Left);
            var rhs = Evaluate(divisionExpression.Right);

            return Divide(lhs, rhs);
        }

        public AnyValue Visit(ModulusExpression modulusExpression)
        {
            var lhs = Evaluate(modulusExpression.Left);
            var rhs = Evaluate(modulusExpression.Right);

            return Modulus(lhs, rhs);
        }

        public AnyValue Visit(MultiplicationExpression multiplicationExpression)
        {
            var lhs = Evaluate(multiplicationExpression.Left);
            var rhs = Evaluate(multiplicationExpression.Right);

            return Multiply(lhs, rhs);
        }

        public AnyValue Visit(SubtractionExpression subtractionExpression)
        {
            var lhs = Evaluate(subtractionExpression.Left);
            var rhs = Evaluate(subtractionExpression.Right);

            return Subtract(lhs, rhs);
        }

        public AnyValue Visit(AndExpression andExpression)
        {
            var lhs = CheckBool(Evaluate(andExpression.Left));

            return lhs ? AnyValue.FromBool(CheckBool(Evaluate(andExpression.Right))) : AnyValue.FromBool(false);
        }

        public AnyValue Visit(EqualExpression equalExpression)
        {
            var lhs = Evaluate(equalExpression.Left);
            var rhs = Evaluate(equalExpression.Right);

            return AnyValue.FromBool(Equals(lhs._value, rhs._value));
        }

        public AnyValue Visit(LessEqualExpression lessEqualExpression)
        {
            var lhs = Evaluate(lessEqualExpression.Left);
            var rhs = Evaluate(lessEqualExpression.Right);

            return AnyValue.FromBool(!IsMore(lhs, rhs));
        }

        public AnyValue Visit(LessExpression lessExpression)
        {
            var lhs = Evaluate(lessExpression.Left);
            var rhs = Evaluate(lessExpression.Right);

            return AnyValue.FromBool(IsLess(lhs, rhs));
        }

        public AnyValue Visit(MoreEqualExpression moreEqualExpression)
        {
            var lhs = Evaluate(moreEqualExpression.Left);
            var rhs = Evaluate(moreEqualExpression.Right);

            return AnyValue.FromBool(!IsLess(lhs, rhs));
        }

        public AnyValue Visit(MoreExpression moreExpression)
        {
            var lhs = Evaluate(moreExpression.Left);
            var rhs = Evaluate(moreExpression.Right);

            return AnyValue.FromBool(IsMore(lhs, rhs));
        }

        public AnyValue Visit(NotEqualExpression notEqualExpression)
        {
            var lhs = Evaluate(notEqualExpression.Left);
            var rhs = Evaluate(notEqualExpression.Right);

            return AnyValue.FromBool(!Equals(lhs._value, rhs._value));
        }

        public AnyValue Visit(OrExpression orExpression)
        {
            var lhs = CheckBool(Evaluate(orExpression.Left));

            if (!lhs)
                return AnyValue.FromBool(CheckBool(Evaluate(orExpression.Right)));

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
            var value = Evaluate(unaryMinusExpression.Expression);

            return value.Type switch
            {
                AnyValueType.Integer => AnyValue.FromInteger(-value.AsInteger()),
                AnyValueType.Float => AnyValue.FromFloat(-value.AsFloat()),
                _ => throw new ValueError($"Operator - not supported for {value.Type}")
            };
        }

        public AnyValue Visit(UnaryNotExpression unaryNotExpression)
        {
            var value = Evaluate(unaryNotExpression.Expression);

            return value.Type switch
            {
                AnyValueType.Bool => AnyValue.FromBool(!value.AsBool()),
                _ => throw new ValueError($"Operator ! not supported for {value.Type}")
            };
        }

        public AnyValue Visit(VariableAdditionExpression variableAdditionExpression)
        {
            var variable = GetVariable(variableAdditionExpression.Variable);
            var value = Evaluate(variableAdditionExpression.Value);

            RegisterVariable(variableAdditionExpression.Variable, Add(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableAssigmentExpression variableAssigmentExpression)
        {
            GetVariable(variableAssigmentExpression.Variable);
            var value = Evaluate(variableAssigmentExpression.Value);

            RegisterVariable(variableAssigmentExpression.Variable, value);

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableDivisionExpression variableDivisionExpression)
        {
            var variable = GetVariable(variableDivisionExpression.Variable);
            var value = Evaluate(variableDivisionExpression.Value);

            RegisterVariable(variableDivisionExpression.Variable, Divide(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableModulusExpression variableModulusExpression)
        {
            var variable = GetVariable(variableModulusExpression.Variable);
            var value = Evaluate(variableModulusExpression.Value);

            RegisterVariable(variableModulusExpression.Variable, Modulus(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableMultiplicationExpression variableMultiplicationExpression)
        {
            var variable = GetVariable(variableMultiplicationExpression.Variable);
            var value = Evaluate(variableMultiplicationExpression.Value);

            RegisterVariable(variableMultiplicationExpression.Variable, Multiply(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(VariableSubtractionExpression variableSubtractionExpression)
        {
            var variable = GetVariable(variableSubtractionExpression.Variable);
            var value = Evaluate(variableSubtractionExpression.Value);

            RegisterVariable(variableSubtractionExpression.Variable, Subtract(variable, value));

            return AnyValue.FromNull();
        }

        public AnyValue Visit(FunctionCallExpression functionCallExpression)
        {
            var function = GetFunction(functionCallExpression.Name);

            var arguments = functionCallExpression.Arguments.
                Select(e => Evaluate(e))
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

            throw new ValueError($"Cannot compare {lhs._value} and {rhs._value}");
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