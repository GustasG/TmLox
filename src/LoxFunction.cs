using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Errors;
using TmLox.Interpreter;
using TmLox.Interpreter.StackUnwinding;

namespace TmLox
{
    public class LoxFunction : IFunction
    {
        private readonly IList<string> _parameters;

        private readonly IList<Statement> _statements;

        public LoxFunction(IList<string> parameters, IList<Statement> statements)
        {
            _parameters = parameters;
            _statements = statements;
        }

        public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            if (_parameters.Count != arguments.Count)
                throw new ValueError($"Expected {_parameters.Count} parameters, got {arguments.Count}");

            for (int i = 0; i < _parameters.Count; i++)
                interpreter.AddVariable(_parameters[i], arguments[i]);

            try
            {
                interpreter.Execute(_statements);
            }
            catch(ReturnUnwind returnValue)
            {
                return returnValue.Value;
            }
            
            return AnyValue.FromNull();
        }
    }
}
