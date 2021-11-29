namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical
{
    public class LessExpression : BinaryLogicalExpression
    {
        public LessExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Less;
        }
    }
}