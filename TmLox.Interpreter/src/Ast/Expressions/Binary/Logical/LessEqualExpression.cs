namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class LessEqualExpression : BinaryExpression
    {
        public override NodeType Type
        {
            get => NodeType.LessEqual;
        }

        public LessEqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}