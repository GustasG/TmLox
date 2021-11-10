using System.Diagnostics;
using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class FunctionDeclarationStatement : Statement
    {
        public string Name { get; }

        public IList<string> Parameters { get; }

        public IList<Statement> Body { get; }


        public FunctionDeclarationStatement(string name)
        {
            Name = name;
            Parameters = new List<string>();
            Body = new List<Statement>();
        }

        public FunctionDeclarationStatement(Token name, IList<string> parameters, IList<Statement> body)
        {
            Debug.Assert(name.TokenType == TokenType.Identifier && name.Value != null, "Token is not a valid identifier");

            Name = name.Value as string;
            Parameters = parameters;
            Body = body;
        }

        public override NodeType Type()
        {
            return NodeType.FunctionDeclaration;
        }
    }
}
