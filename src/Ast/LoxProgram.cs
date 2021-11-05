using System.Collections.Generic;

namespace TmLox.Ast
{
    public class LoxProgram
    {
        public IList<Statement> Statements { get; }


        public LoxProgram()
        {
            Statements = new List<Statement>();
        }

        public LoxProgram(IList<Statement> statements)
        {
            Statements = statements;
        }
    }
}