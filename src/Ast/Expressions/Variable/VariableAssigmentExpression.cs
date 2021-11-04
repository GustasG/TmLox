namespace TmLox.Ast.Expressions.Variable
{
    public class VariableAssigmentExpression : VariableModificationExpression
    {
        public VariableAssigmentExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableAssigmentExpression(VariableExpression variable, Expression value)
            : base(variable, value)
        {
        }
    }
}