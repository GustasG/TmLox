using System;
using System.Collections.Generic;


namespace TmLox.Interpreter.Functions.Native
{
    public class PrintFunction : ICallable
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

            return AnyValue.CreateNull();
        }
    }
}