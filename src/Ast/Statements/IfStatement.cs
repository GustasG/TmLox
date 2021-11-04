using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class ElseIfStatement : Statement
    {
        public Expression Condition { get; }

        public IList<Statement> Statements { get; }


        public ElseIfStatement(Expression condition, IList<Statement> statements)
        {
            Condition = condition;
            Statements = statements;
        }
    }


    public class IfStatement : Statement
    {
        public Expression Condition { get; }

        public IList<Statement> Statements { get; }

        public IList<ElseIfStatement> ElseIfStatements { get; }

        public IList<Statement> ElseStatements { get; }


        public IfStatement(Expression condition, IList<Statement> statements)
        {
            Condition = condition;
            Statements = statements;
            ElseIfStatements = new List<ElseIfStatement>();
            ElseStatements = new List<Statement>();
        }

        public IfStatement(Expression condition, IList<Statement> statements, IList<ElseIfStatement> elseIfStatements)
        {
            Condition = condition;
            Statements = statements;
            ElseIfStatements = elseIfStatements;
            ElseStatements = new List<Statement>();
        }

        public IfStatement(Expression condition, IList<Statement> statements, IList<ElseIfStatement> elseIfStatements, IList<Statement> elseStatements)
        {
            Condition = condition;
            Statements = statements;
            ElseIfStatements = elseIfStatements;
            ElseStatements = elseStatements;
        }
    }
}