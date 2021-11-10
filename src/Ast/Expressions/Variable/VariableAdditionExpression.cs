namespace TmLox.Ast.Expressions.Variable
{
    public class VariableAdditionExpression : VariableModificationExpression
    {
        public VariableAdditionExpression(string variable, Expression value) 
            : base(variable, value)
        {
        }

        public VariableAdditionExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }

        public override NodeType Type()
        {
            return NodeType.VariableAddition;
        }
    }
}