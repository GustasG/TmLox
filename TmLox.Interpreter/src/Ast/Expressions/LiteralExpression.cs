namespace TmLox.Interpreter.Ast.Expressions
{
    public class LiteralExpression : Expression
    {
        public override NodeType Type
        {
            get => NodeType.Literal;
        }


        public AnyValue Value { get; }

        public LiteralExpression(AnyValue value)
        {
            Value = value;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}