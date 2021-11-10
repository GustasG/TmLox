using System;
using System.IO;

using Newtonsoft.Json;

using TmLox.Ast;
using TmLox.Errors;

namespace TmLox
{
    class Lox
    {
        public static void Main(string[] args)
        {
            var path = "examples/sandbox.lox";
            var source = File.ReadAllText(path);

            try
            {
                var lexer = new Lexer(source);
                var parser = new Parser(lexer);
                var program = parser.Run();

                PrintAst(program);
            }
            catch (LoxException e)
            {
                Console.Error.WriteLine("{0}: {1}", path, e.Message);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
            }
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