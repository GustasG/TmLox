using System;
using System.IO;
using TmLox.Errors;
using System.Collections.Generic;

namespace TmLox
{
    class Program
    {
        public static void Main(string[] args)
        {
            var path = "examples/sandbox.lox";
            var source = File.ReadAllText(path);

            try
            {
                var lexer = new Lexer(source);
                var parser = new Parser(lexer);
                parser.Run();

            }
            catch (LoxException e)
            {
                Console.WriteLine("file: {0}: {1}", path, e.Message);
            }
        }

        private static void PrintTokens(IEnumerable<Token> tokenStream)
        {
            var fmt = "| {0,7} | {1,10} | {2,15} | {3,20} |";

            Console.WriteLine(fmt, "LINE", "COLUMN", "TYPE", "VALUE");

            foreach(var token in tokenStream)
            {
                Console.WriteLine(fmt, token.Line, token.Column, token.Lexeme, token.Value);
            }
        }
    }
}
