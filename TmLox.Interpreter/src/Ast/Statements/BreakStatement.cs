namespace TmLox.Interpreter.Ast.Statements;

public class BreakStatement : Statement
{
    public override NodeType Type => NodeType.Break;

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}