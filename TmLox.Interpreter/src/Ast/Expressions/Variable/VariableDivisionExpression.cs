namespace TmLox.Ast.Expressions.Variable
{
    public class VariableDivisionExpression : VariableModificationExpression
    {
        public VariableDivisionExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableDivisionExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.VariableDivision;
        }
    }
}