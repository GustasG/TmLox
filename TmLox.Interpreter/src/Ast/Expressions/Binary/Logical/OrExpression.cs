namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class OrExpression : BinaryExpression
    {
        public override NodeType Type
        {
            get => NodeType.Or;
        }


        public OrExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}