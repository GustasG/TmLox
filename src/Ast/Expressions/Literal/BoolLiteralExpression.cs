namespace TmLox.Ast.Expressions.Literal
{
    public class BoolLiteralExpression : LiteralExpression<bool>
    {
        public BoolLiteralExpression(bool value)
            : base(value)
        {
        }

        public override NodeType Type()
        {
            return NodeType.BoolLiteral;
        }
    }
}