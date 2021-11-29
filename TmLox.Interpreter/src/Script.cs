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
                if (_interpreter.TryGet(name, out var value))
                    return value;

                throw new KeyNotFoundException($"Variable with name {name} does not exist");
            }
            set => _interpreter.AddVariable(name, value);
        }


        private readonly IInterpreter _interpreter;

        public Script()
        {
            _interpreter = new TreeWalkingInterpreter();

            _interpreter.AddFunction(new PrintFunction());
            _interpreter.AddFunction(new ClockFunction());
        }

        public void RunString(string code)
        {
            var statements = CreateAst(code);

            RegisterGlobals(statements);
            _interpreter.Execute(statements);
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
                switch (statement.Type())
                {
                    case NodeType.FunctionDeclaration:
                        var functionDeclaration = statement as FunctionDeclarationStatement;
                        _interpreter.AddFunction(new LoxFunction(functionDeclaration));
                        break;
                }
            }

            foreach (var statement in statements)
            {
                switch (statement.Type())
                {
                    case NodeType.VariableDeclaration:
                        var variableDeclaration = statement as VariableDeclarationStatement;
                        var value = variableDeclaration.Value != null ? _interpreter.Evaluate(variableDeclaration.Value) : AnyValue.CreateNull();
                        _interpreter.AddVariable(variableDeclaration.Name, value);
                        break;
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
