using System;
using System.IO;
using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Interpreter;
using TmLox.Functions.Native;

namespace TmLox
{
    class Lox
    {
        public static int Main(string[] args)
        {
            var interpreter = new TreeWalkingInterpreter();
            interpreter.RegisterFunction("print", new PrintFunction());

            try
            {
                var path = "Examples/sandbox.lox";
                var source = File.ReadAllText(path);

                var statements = CreateAst(source);
                interpreter.Execute(statements);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return e.HResult;
            }

            return 0;
        }

        private static IList<Statement> CreateAst(string source)
        {
            var lexer = new Lexer(source);
            var parser = new Parser(lexer);
            var statements = parser.Parse();

            return statements;
        }
    }
}