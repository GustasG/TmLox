namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class AndExpression : BinaryExpression
    {
        public override NodeType Type
        {
            get => NodeType.And;
        }


        public AndExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}