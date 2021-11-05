using System;
using System.IO;
using System.Collections.Generic;

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
                var lexer = new LoxLexer(source);
                var parser = new LoxParser(lexer);
                var program = parser.Run();

                PrintAst(program);
            }
            catch (LoxException e)
            {
                Console.WriteLine("{0}: {1}", path, e.Message);
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

        private static void PrintTokens(IEnumerable<Token> tokenStream)
        {
            var fmt = "| {0,7} | {1,10} | {2,15} | {3,20} |";

            Console.WriteLine(fmt, "LINE", "COLUMN", "TYPE", "VALUE");

            foreach(var token in tokenStream)
            {
                Console.WriteLine(fmt, token.Line, token.Column, token.TokenType, token.Value);
            }
        }
    }
}