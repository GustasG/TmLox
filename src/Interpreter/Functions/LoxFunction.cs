using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Errors;
using TmLox.Interpreter;
using TmLox.Interpreter.StackUnwinding;

namespace TmLox.Interpreter.Functions
{
    public class LoxFunction : IFunction
    {
        private readonly IList<string> _parameters;

        private readonly IList<Statement> _body;

        public LoxFunction(IList<string> parameters, IList<Statement> body)
        {
            _parameters = parameters;
            _body = body;
        }

        public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            if (_parameters.Count != arguments.Count)
                throw new ValueError($"Expected {_parameters.Count} parameters, got {arguments.Count}");

            for (int i = 0; i < _parameters.Count; i++)
                interpreter.RegisterVariable(_parameters[i], arguments[i]);

            try
            {
                interpreter.Execute(_body);
            }
            catch(ReturnUnwind returnValue)
            {
                return returnValue.Value;
            }
            
            return AnyValue.FromNull();
        }
    }
}
