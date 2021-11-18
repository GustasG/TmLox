using System;
using System.Collections.Generic;

namespace TmLox
{
    public class PrintFunction : IFunction
    {
        public AnyValue Call(IEnumerable<AnyValue> arguments)
        {
            foreach (var argument in arguments)
            {
                Console.Write(argument);
            }

            Console.WriteLine();

            return AnyValue.FromNull();
        }
    }
}
