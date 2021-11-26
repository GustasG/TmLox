namespace TmLox.Ast.Expressions.Variable
{
    public class VariableModulusExpression : VariableModificationExpression
    {
        public VariableModulusExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableModulusExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.VariableModulus;
        }
    }
}