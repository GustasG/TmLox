using System;
using System.IO;

using Newtonsoft.Json;

using TmLox.Ast;

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
                var program = parser.Run();

                PrintAst(program);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
                return e.HResult;
            }

            return 0;
        }

        private static void PrintAst(LoxProgram program)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            Console.WriteLine(JsonConvert.SerializeObject(program, settings));
        }
    }
}