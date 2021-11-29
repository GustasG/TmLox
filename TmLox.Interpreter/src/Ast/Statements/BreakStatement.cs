namespace TmLox.Interpreter.Ast.Statements
{
    public class BreakStatement : Statement
    {
        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Break;
        }
    }
}
