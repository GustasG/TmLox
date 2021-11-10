namespace TmLox.Ast.Statements
{
    public class BreakStatement : Statement
    {
        public override NodeType Type()
        {
            return NodeType.Break;
        }
    }
}
