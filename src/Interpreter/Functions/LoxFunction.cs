using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Ast.Statements;
using TmLox.Interpreter.StackUnwinding;

namespace TmLox.Interpreter.Functions
{
    public class LoxFunction : ICallable
    {
        private readonly IList<string> _parameters;

        private readonly IList<Statement> _body;


        public LoxFunction(IList<string> parameters, IList<Statement> body)
        {
            _parameters = parameters;
            _body = body;
        }

        public LoxFunction(FunctionDeclarationStatement functionDeclarationStatement)
            : this(functionDeclarationStatement.Parameters, functionDeclarationStatement.Body)
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
                interpreter.AddVariable(_parameters[i], arguments[i]);

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
    }
}
