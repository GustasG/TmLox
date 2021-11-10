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

        public override NodeType Type()
        {
            return NodeType.VariableModulus;
        }
    }
}