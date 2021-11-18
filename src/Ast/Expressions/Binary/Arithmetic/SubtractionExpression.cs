namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class SubtractionExpression : BinaryArithmeticExpression
    {
        public SubtractionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Subtraction;
        }
    }
}
