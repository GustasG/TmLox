using System;
using System.Collections.Generic;

using TmLox.Interpreter;

namespace TmLox
{
    public class PrintFunction : IFunction
    {
        public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                Console.Write(arguments[i]);

                if (i != arguments.Count - 1)
                    Console.Write(' ');
            }

            Console.WriteLine();

            return AnyValue.FromNull();
        }
    }
}
