namespace TmLox.Ast.Expressions.Literal
{
    public class FloatLiteralExpression : LiteralExpression<double>
    {
        public FloatLiteralExpression(double value)
            : base(value)
        {
        }

        public FloatLiteralExpression(Token token)
            : this((double)token.Value)
        {
        }

        public override NodeType Type()
        {
            return NodeType.FloatLiteral;
        }
    }
}