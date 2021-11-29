namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class AndExpression : BinaryLogicalExpression
    {
        public AndExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.And;
        }
    }
}