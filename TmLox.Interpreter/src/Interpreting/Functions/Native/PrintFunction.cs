using System;
using System.Collections.Generic;


namespace TmLox.Interpreter.Functions.Native
{
    public class PrintFunction : NativeFunction
    {
        public PrintFunction()
            : base("print")
        {
        }
        
        public override bool CheckArity()
        {
            return false;
        }

        public override int Arity()
        {
            return -1;
        }

        public override AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
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