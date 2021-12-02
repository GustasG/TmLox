namespace TmLox.Interpreter.Ast.Expressions.Binary.Arithmetic
{
    public class SubtractionExpression : BinaryExpression
    {
        public override NodeType Type
        {
            get => NodeType.Subtraction;
        }


        public SubtractionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}