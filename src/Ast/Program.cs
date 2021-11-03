using System.Collections.Generic;

namespace TmLox.Ast
{
    public class Program
    {
        public IList<Statement> Statements { get; }


        public Program()
        {
            Statements = new List<Statement>();
        }

        public Program(IList<Statement> statements)
        {
            Statements = statements;
        }
    }
}
