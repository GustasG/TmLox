using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class ClassStatement : Statement
    {
        public string Name { get; }

        public IList<string> Inherited;


        public ClassStatement(string name, IList<string> inherited)
        {
            Name = name;
            Inherited = inherited;
        }

        public ClassStatement(Token token, IList<string> inherited)
            : this(token.Value as string, inherited)
        {
        }
    }
}