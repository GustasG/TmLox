using System;
using System.IO;

namespace TmLox
{
    class Lox
    {
        public static int Main(string[] args)
        {
            var path = "Examples/sandbox.lox";
            var source = File.ReadAllText(path);

            try
            {
                var lexer = new Lexer(source);
                var parser = new Parser(lexer);
                var statements = parser.Parse();

                var interpreter = new TreeWalkingInterpreter();
                interpreter.Interpret(statements);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return e.HResult;
            }

            return 0;
        }
    }
}