namespace TmLox.Interpreter.Ast.Expressions
{
    public class LiteralExpression : Expression
    {
        public AnyValue Value { get; }

        public LiteralExpression(AnyValue value)
        {
            Value = value;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Literal;
        }
    }
}