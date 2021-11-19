using System;
using System.IO;
using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Interpreter;
using TmLox.Interpreter.Functions.Native;
using TmLox.Ast.Statements;
using TmLox.Interpreter.Functions;

namespace TmLox
{
    class Lox
    {
        public static int Main(string[] args)
        {
            var interpreter = new TreeWalkingInterpreter();
            interpreter.RegisterFunction("print", new PrintFunction());
            interpreter.RegisterFunction("clock", new ClockFunction());

            try
            {
                var path = "Examples/sandbox.lox";
                var source = File.ReadAllText(path);

                var statements = CreateAst(source);
                RegisterGlobals(interpreter, statements);

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

        private static void RegisterGlobals(IInterpreter interpreter, IList<Statement> statements)
        {
            foreach (var statement in statements)
            {
                switch (statement.Type())
                {
                    case NodeType.FunctionDeclaration:
                        var functionDeclaration = statement as FunctionDeclarationStatement;
                        interpreter.RegisterFunction(functionDeclaration.Name, new LoxFunction(functionDeclaration.Parameters, functionDeclaration.Body));
                        break;
                }
            }
        }
    }
}