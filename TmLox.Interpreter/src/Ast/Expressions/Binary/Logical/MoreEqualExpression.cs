namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class MoreEqualExpression : BinaryLogicalExpression
    {
        public MoreEqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.MoreEqual;
        }
    }
}
