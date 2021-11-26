namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class DivisionExpression : BinaryArithmeticExpression
    {
        public DivisionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Division;
        }
    }
}
