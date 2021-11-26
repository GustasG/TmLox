using System;
using System.IO;
using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Interpreter;
using TmLox.Ast.Statements;
using TmLox.Interpreter.Functions;
using TmLox.Interpreter.Functions.Native;

namespace TmLox
{
    class Lox
    {
        public static int Main(string[] args)
        {
            var interpreter = new TreeWalkingInterpreter();
            interpreter.AddFunction(new PrintFunction());
            interpreter.AddFunction(new ClockFunction());

            try
            {
                var path = "Examples/sandbox.lox";
                var source = File.ReadAllText(path);

                var statements = CreateAst(source);
//                RegisterGlobals(interpreter, statements);

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

        /*
        private static void RegisterGlobals(IInterpreter interpreter, IList<Statement> statements)
        {
            // First register functions, then variables
            // Because global variables might be initialized with function return value
            // So all functions have to be defined beforehand

            foreach (var statement in statements)
            {
                switch (statement.Type())
                {
                    case NodeType.FunctionDeclaration:
                        var functionDeclaration = statement as FunctionDeclarationStatement;
                        interpreter.AddFunction(new LoxFunction(functionDeclaration));
                        break;
                }
            }

            foreach (var statement in statements)
            {
                switch (statement.Type())
                {
                    case NodeType.VariableDeclaration:
                        var variableDeclaration = statement as VariableDeclarationStatement;
                        interpreter.AddVariable(variableDeclaration.Name, interpreter.Evaluate(variableDeclaration.Value));
                        break;
                }
            }
        }
        */
    }
}