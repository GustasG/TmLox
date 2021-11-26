using System;
using System.Linq;
using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Errors;
using TmLox.Ast.Statements;
using TmLox.Ast.Expressions;
using TmLox.Interpreter.Functions;
using TmLox.Ast.Expressions.Unary;
using TmLox.Ast.Expressions.Variable;
using TmLox.Interpreter.StackUnwinding;
using TmLox.Ast.Expressions.Binary.Logical;
using TmLox.Ast.Expressions.Binary.Arithmetic;

namespace TmLox.Interpreter
{
    public class TreeWalkingInterpreter : IInterpreter, IVisitor<AnyValue>
    {
        private Environment _currentEnvironment;

        public TreeWalkingInterpreter()
        {
            _currentEnvironment = new Environment();
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

        public void AddVariable(string name, AnyValue value)
        {
            _currentEnvironment.Add(name, value);
        }

        public void AddFunction(string name, ICallable function)
        {
            _currentEnvironment.Add(name, AnyValue.CreateFunction(function));
        }

        public AnyValue Visit(BreakStatement breakStatement)
        {
            throw new BreakUnwind();
        }

        public AnyValue Visit(ForStatement forStatement)
        {
            var currentEnviroment = _currentEnvironment;
            _currentEnvironment = new Environment(_currentEnvironment);

            try
            {
                if (forStatement.Initial != null)
                    Execute(forStatement.Initial);

                // TODO: If condition is null this will crash interpreter. Figure out what to do when condition is not existant.
                while (CheckBool(Evaluate(forStatement.Condition)))
                {
                    Execute(forStatement.Body);

                    if (forStatement.Increment != null)
                        Execute(forStatement.Increment);
                }
            }
            catch (BreakUnwind)
            {

            }
            finally
            {
                _currentEnvironment = currentEnviroment;
            }

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(FunctionDeclarationStatement functionDeclarationStatement)
        {
            var function = new LoxFunction(functionDeclarationStatement);
            _currentEnvironment.Add(functionDeclarationStatement.Name, AnyValue.CreateFunction(function));

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(IfStatement ifStatement)
        {
            if (!VisitIf(ifStatement) && !VisitElif(ifStatement) && ifStatement.ElseBody != null)
            {
                var currentEnviroment = _currentEnvironment;
                _currentEnvironment = new Environment(_currentEnvironment);

                try
                {
                    Execute(ifStatement.ElseBody);
                }
                finally
                {
                    _currentEnvironment = currentEnviroment;
                }
            }

            return AnyValue.CreateNull();
        }

        private bool VisitIf(IfStatement ifStatement)
        {
            var currentEnviroment = _currentEnvironment;
            _currentEnvironment = new Environment(_currentEnvironment);

            try
            {
                if (CheckBool(Evaluate(ifStatement.Condition)))
                {
                    Execute(ifStatement.Body);
                    return true;
                }
            }
            finally
            {
                _currentEnvironment = currentEnviroment;
            }

            return false;
        }

        private bool VisitElif(IfStatement elifStatement)
        {
            foreach (var elif in elifStatement.ElseIfStatements)
            {
                var currentEnviroment = _currentEnvironment;
                _currentEnvironment = new Environment(_currentEnvironment);

                try
                {
                    if (CheckBool(Evaluate(elif.Condition)))
                    {
                        Execute(elif.Body);
                        return true;
                    }
                }
                finally
                {
                    _currentEnvironment = currentEnviroment;
                }
            }

            return false;
        }

        public AnyValue Visit(ElseIfStatement elseIfStatement)
        {
            throw new NotImplementedException();
        }

        public AnyValue Visit(ReturnStatement returnStatement)
        {
            var value = AnyValue.CreateNull();

            if (returnStatement.Value != null)
                value = Evaluate(returnStatement.Value);

            throw new ReturnUnwind(value);
        }

        public AnyValue Visit(VariableDeclarationStatement variableDeclarationStatement)
        {
            var value = variableDeclarationStatement.Value != null ? Evaluate(variableDeclarationStatement.Value) : AnyValue.CreateNull();
            _currentEnvironment.Add(variableDeclarationStatement.Name, value);

            return value;
        }

        public AnyValue Visit(WhileStatement whileStatement)
        {
            var currentEnviroment = _currentEnvironment;
            _currentEnvironment = new Environment(_currentEnvironment);

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
                _currentEnvironment = currentEnviroment;
            }

            return AnyValue.CreateNull();
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

            return lhs ? AnyValue.CreateBool(CheckBool(Evaluate(andExpression.Right))) : AnyValue.CreateBool(false);
        }

        public AnyValue Visit(EqualExpression equalExpression)
        {
            var lhs = Evaluate(equalExpression.Left);
            var rhs = Evaluate(equalExpression.Right);

            return AnyValue.CreateBool(lhs == rhs);
        }

        public AnyValue Visit(LessEqualExpression lessEqualExpression)
        {
            var lhs = Evaluate(lessEqualExpression.Left);
            var rhs = Evaluate(lessEqualExpression.Right);

            return AnyValue.CreateBool(!IsMore(lhs, rhs));
        }

        public AnyValue Visit(LessExpression lessExpression)
        {
            var lhs = Evaluate(lessExpression.Left);
            var rhs = Evaluate(lessExpression.Right);

            return AnyValue.CreateBool(IsLess(lhs, rhs));
        }

        public AnyValue Visit(MoreEqualExpression moreEqualExpression)
        {
            var lhs = Evaluate(moreEqualExpression.Left);
            var rhs = Evaluate(moreEqualExpression.Right);

            return AnyValue.CreateBool(!IsLess(lhs, rhs));
        }

        public AnyValue Visit(MoreExpression moreExpression)
        {
            var lhs = Evaluate(moreExpression.Left);
            var rhs = Evaluate(moreExpression.Right);

            return AnyValue.CreateBool(IsMore(lhs, rhs));
        }

        public AnyValue Visit(NotEqualExpression notEqualExpression)
        {
            var lhs = Evaluate(notEqualExpression.Left);
            var rhs = Evaluate(notEqualExpression.Right);

            return AnyValue.CreateBool(lhs != rhs);
        }

        public AnyValue Visit(OrExpression orExpression)
        {
            var lhs = CheckBool(Evaluate(orExpression.Left));

            return !lhs ? AnyValue.CreateBool(CheckBool(Evaluate(orExpression.Right))) : AnyValue.CreateBool(true);
        }

        public AnyValue Visit(LiteralExpression literalExpression)
        {
            return literalExpression.Value;
        }
        public AnyValue Visit(UnaryMinusExpression unaryMinusExpression)
        {
            var value = Evaluate(unaryMinusExpression.Expression);

            return value.Type switch
            {
                AnyValueType.Integer => AnyValue.CreateInteger(-value.AsInteger()),
                AnyValueType.Float => AnyValue.CreateFloat(-value.AsFloat()),
                _ => throw new ValueError($"Unary - not supported for {value.Type}")
            };
        }

        public AnyValue Visit(UnaryNotExpression unaryNotExpression)
        {
            var value = Evaluate(unaryNotExpression.Expression);

            return value.Type switch
            {
                AnyValueType.Bool => AnyValue.CreateBool(!value.AsBool()),
                _ => throw new ValueError($"Unary ! not supported for {value.Type}")
            };
        }

        public AnyValue Visit(VariableAdditionExpression variableAdditionExpression)
        {
            var variable = GetVariable(variableAdditionExpression.Variable);
            var value = Evaluate(variableAdditionExpression.Value);

            _currentEnvironment.Assign(variableAdditionExpression.Variable, Add(variable, value));

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(VariableAssigmentExpression variableAssigmentExpression)
        {
            GetVariable(variableAssigmentExpression.Variable);
            var value = Evaluate(variableAssigmentExpression.Value);

            _currentEnvironment.Assign(variableAssigmentExpression.Variable, value);

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(VariableDivisionExpression variableDivisionExpression)
        {
            var variable = GetVariable(variableDivisionExpression.Variable);
            var value = Evaluate(variableDivisionExpression.Value);

            _currentEnvironment.Assign(variableDivisionExpression.Variable, Divide(variable, value));

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(VariableModulusExpression variableModulusExpression)
        {
            var variable = GetVariable(variableModulusExpression.Variable);
            var value = Evaluate(variableModulusExpression.Value);

            _currentEnvironment.Assign(variableModulusExpression.Variable, Modulus(variable, value));

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(VariableMultiplicationExpression variableMultiplicationExpression)
        {
            var variable = GetVariable(variableMultiplicationExpression.Variable);
            var value = Evaluate(variableMultiplicationExpression.Value);

            _currentEnvironment.Assign(variableMultiplicationExpression.Variable, Multiply(variable, value));

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(VariableSubtractionExpression variableSubtractionExpression)
        {
            var variable = GetVariable(variableSubtractionExpression.Variable);
            var value = Evaluate(variableSubtractionExpression.Value);

            _currentEnvironment.Assign(variableSubtractionExpression.Variable, Subtract(variable, value));

            return AnyValue.CreateNull();
        }

        public AnyValue Visit(FunctionCallExpression functionCallExpression)
        {
            var function = GetFunction(functionCallExpression.Name);

            var arguments = functionCallExpression.Arguments.
                Select(e => Evaluate(e))
                .ToList();

            if (function.CheckArity() && function.Arity() != arguments.Count)
                throw new ValueError($"Function {functionCallExpression.Name} expects {function.Arity()} arguments, while {arguments.Count} were provided");

            var currentEnviroment = _currentEnvironment;
            _currentEnvironment = new Environment(_currentEnvironment);

            try
            {
                return function.Call(this, arguments);
            }
            finally
            {
                _currentEnvironment = currentEnviroment;
            }
        }

        public AnyValue Visit(VariableExpression variableExpression)
        {
            return GetVariable(variableExpression.Name);
        }

        private AnyValue GetVariable(string name)
        {
            if (_currentEnvironment.TryGet(name, out var variable))
                return variable;

            throw new ValueError($"Variable {name} does not exist");
        }

        private ICallable GetFunction(string name)
        {
            if (_currentEnvironment.TryGet(name, out var variable))
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
                return AnyValue.CreateInteger(lhs.AsInteger() + rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.CreateFloat(lhs.AsFloat() + rhs.AsFloat());

            throw new ValueError($"Operator + not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Subtract(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.CreateInteger(lhs.AsInteger() - rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.CreateFloat(lhs.AsFloat() - rhs.AsFloat());

            throw new ValueError($"Operator - not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Multiply(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.CreateInteger(lhs.AsInteger() * rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.CreateFloat(lhs.AsFloat() * rhs.AsFloat());

            throw new ValueError($"Operator * not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Divide(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.CreateFloat(lhs.AsFloat() / rhs.AsFloat());

            throw new ValueError($"Operator / not supported for {lhs.Type} and {rhs.Type}");
        }

        private static AnyValue Modulus(AnyValue lhs, AnyValue rhs)
        {
            if (lhs.IsInteger() && rhs.IsInteger())
                return AnyValue.CreateInteger(lhs.AsInteger() % rhs.AsInteger());
            else if (lhs.IsNumber() && rhs.IsNumber())
                return AnyValue.CreateFloat(lhs.AsFloat() % rhs.AsFloat());

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