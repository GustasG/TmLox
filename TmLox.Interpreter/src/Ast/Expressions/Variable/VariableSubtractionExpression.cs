namespace TmLox.Ast.Expressions.Variable
{
    public class VariableSubtractionExpression : VariableModificationExpression
    {
        public VariableSubtractionExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableSubtractionExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.VariableSubtraction;
        }
    }
}