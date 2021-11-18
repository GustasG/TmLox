namespace TmLox.Ast.Expressions.Literal
{
    public class BoolLiteralExpression : LiteralExpression<bool>
    {
        public BoolLiteralExpression(bool value)
            : base(value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.BoolLiteral;
        }
    }
}