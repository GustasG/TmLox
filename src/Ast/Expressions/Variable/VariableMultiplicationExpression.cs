namespace TmLox.Ast.Expressions.Variable
{
    public class VariableMultiplicationExpression : VariableModificationExpression
    {
        public VariableMultiplicationExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableMultiplicationExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }

        public override NodeType Type()
        {
            return NodeType.VariableMultiplication;
        }
    }
}