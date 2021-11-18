using System;
using System.IO;

using TmLox.Interpreter;

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
                var interpreter = new TreeWalkingInterpreter();
                interpreter.AddVariable("print", AnyValue.FromFunction(new PrintFunction()));

                var lexer = new Lexer(source);
                var parser = new Parser(lexer);
                var statements = parser.Parse();

                interpreter.Execute(statements);
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