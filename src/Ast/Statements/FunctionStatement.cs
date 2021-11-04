using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class FunctionStatement : Statement
    {
        public string Name { get; }

        public IList<string> Parameters { get; }

        public IList<Statement> Body { get; }


        public FunctionStatement(string name)
        {
            Name = name;
            Parameters = new List<string>();
            Body = new List<Statement>();
        }

        public FunctionStatement(string name, IList<string> parameters, IList<Statement> body)
        {
            Name = name;
            Parameters = parameters;
            Body = body;
        }

        public FunctionStatement(Token name, IList<string> parameters, IList<Statement> body)
            : this(name.Value as string, parameters, body)
        {
        }
    }
}
