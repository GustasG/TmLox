using System.IO;
using System.Collections.Generic;

using TmLox.Interpreter.Ast;
using TmLox.Interpreter.Ast.Statements;
using TmLox.Interpreter.Execution;
using TmLox.Interpreter.Execution.Functions;
using TmLox.Interpreter.Execution.Functions.Native;

namespace TmLox.Interpreter
{
    public class Script
    {
        public AnyValue this[string name]
        {
            get
            {
                if (Interpreter.TryGet(name, out var value))
                {
                    return value;
                }

                throw new KeyNotFoundException($"Variable with name {name} does not exist");
            }
            set => Interpreter.Add(name, value);
        }

        public IInterpreter Interpreter { get; }

        
        public Script()
        {
            Interpreter = new TreeWalkingInterpreter();

            Interpreter.Add(new PrintFunction());
            Interpreter.Add(new ClockFunction());
        }

        public void RunString(string code)
        {
            var statements = CreateAst(code);

            RegisterGlobals(statements);
            Interpreter.Execute(statements);
        }

        public void RunFile(string path)
        {
            RunString(File.ReadAllText(path));
        }

        private void RegisterGlobals(IList<Statement> statements)
        {
            // First register functions, then variables
            // Because global variables might be initialized with function return value
            // So all functions have to be defined beforehand

            foreach (var statement in statements)
            {
                if (statement.Type == NodeType.FunctionDeclaration)
                {
                    Interpreter.Execute(statement);
                }
            }

            foreach (var statement in statements)
            {
                if (statement.Type == NodeType.VariableDeclaration)
                {
                    Interpreter.Execute(statement);
                }
            }
        }

        private static IList<Statement> CreateAst(string code)
        {
            var lexer = new Lexer(code);
            var parser = new Parser(lexer);
            var statements = parser.Parse();

            return statements;
        }
    }
}
