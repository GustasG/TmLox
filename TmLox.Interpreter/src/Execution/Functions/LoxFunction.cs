using System.Collections.Generic;

using TmLox.Interpreter.Ast;
using TmLox.Interpreter.Ast.Statements;
using TmLox.Interpreter.Execution.StackUnwinding;

namespace TmLox.Interpreter.Execution.Functions
{
    internal class LoxFunction : ICallable
    {
        public string Name { get; }

        public Environment? Environment { set; get; }

        private readonly IList<string> _parameters;

        private readonly IList<Statement> _body;


        public LoxFunction(string name, IList<string> parameters, IList<Statement> body)
        {
            Name = name;
            _parameters = parameters;
            _body = body;
        }

        public LoxFunction(Environment environment, string name, IList<string> parameters, IList<Statement> body)
        {
            Name = name;
            Environment = environment;
            _parameters = parameters;
            _body = body;
        }

        public LoxFunction(FunctionDeclarationStatement functionDeclarationStatement)
            : this(functionDeclarationStatement.Name, functionDeclarationStatement.Parameters, functionDeclarationStatement.Body)
        {
        }

        public LoxFunction(Environment enviroment, FunctionDeclarationStatement functionDeclarationStatement)
            : this(enviroment, functionDeclarationStatement.Name, functionDeclarationStatement.Parameters, functionDeclarationStatement.Body)
        {
        }

        public bool CheckArity()
        {
            return true;
        }

        public int Arity()
        {
            return _parameters.Count;
        }

        public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            for (int i = 0; i < _parameters.Count; i++)
            {
                interpreter.AddVariable(_parameters[i], arguments[i]);
            }

            try
            {
                interpreter.Execute(_body);
            }
            catch (ReturnUnwind returnValue)
            {
                return returnValue.Value;
            }
            
            return AnyValue.CreateNull();
        }

        public override string ToString()
        {
            return $"<function {Name}>";
        }
    }
}
