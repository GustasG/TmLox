namespace TmLox.Interpreter.Ast.Expressions.Binary.Arithmetic
{
    public class AdditionExpression : BinaryExpression
    {
        public AdditionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Addition;
        }
    }
}
