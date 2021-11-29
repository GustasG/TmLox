namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class OrExpression : BinaryLogicalExpression
    {
        public OrExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Or;
        }
    }
}
