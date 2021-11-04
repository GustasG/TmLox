namespace TmLox.Ast.Expressions.Variable
{
    public class VariableDivisionExpression : VariableModificationExpression
    {
        public VariableDivisionExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableDivisionExpression(VariableExpression variable, Expression value)
            : base(variable, value)
        {
        }
    }
}