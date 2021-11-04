namespace TmLox.Ast.Expressions.Variable
{
    public class VariableAdditionExpression : VariableModificationExpression
    {
        public VariableAdditionExpression(string variable, Expression value) 
            : base(variable, value)
        {
        }

        public VariableAdditionExpression(VariableExpression variable, Expression value)
            : base(variable, value)
        {
        }
    }
}