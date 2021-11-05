using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class ElseIfStatement : Statement
    {
        public Expression Condition { get; }

        public IList<Statement> Body { get; }


        public ElseIfStatement(Expression condition, IList<Statement> body)
        {
            Condition = condition;
            Body = body;
        }
    }


    public class IfStatement : Statement
    {
        public Expression Condition { get; }

        public IList<Statement> Body { get; }

        public IList<ElseIfStatement>? ElseIfStatements { get; }

        public IList<Statement>? ElseBody { get; }


        public IfStatement(Expression condition, IList<Statement> statements)
        {
            Condition = condition;
            Body = statements;
        }

        public IfStatement(Expression condition, IList<Statement> body, IList<ElseIfStatement> elseIfStatements)
        {
            Condition = condition;
            Body = body;
            ElseIfStatements = elseIfStatements;
        }

        public IfStatement(Expression condition, IList<Statement> body, IList<ElseIfStatement> elseIfStatements, IList<Statement> elseBody)
        {
            Condition = condition;
            Body = body;
            ElseIfStatements = elseIfStatements;
            ElseBody = elseBody;
        }
    }
}