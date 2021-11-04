using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class ClassDeclarationStatement : Statement
    {
        public string Name { get; }

        public IList<string> Inherited;


        public ClassDeclarationStatement(string name, IList<string> inherited)
        {
            Name = name;
            Inherited = inherited;
        }

        public ClassDeclarationStatement(Token token, IList<string> inherited)
            : this(token.Value as string, inherited)
        {
        }
    }
}