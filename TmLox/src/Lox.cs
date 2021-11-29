using System;

using TmLox.Interpreter;

namespace TmLox
{
    class Lox
    {
        public static int Main(string[] args)
        {
            var script = new Script();

            try
            {
                var path = "Examples/sandbox.lox";

                script.RunFile(path);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return e.HResult;
            }

            return 0;
        }
    }
}