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

        public FunctionDeclarationStatement(string name, IList<string> parameters, IList<Statement> body)
        {
            Name = name;
            Parameters = parameters;
            Body = body;
        }

        public FunctionDeclarationStatement(Token name, IList<string> parameters, IList<Statement> body)
            : this(name.Value as string, parameters, body)
        {
        }

        public override NodeType Type()
        {
            return NodeType.FunctionDeclaration;
        }
    }
}
