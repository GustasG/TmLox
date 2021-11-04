namespace TmLox.Ast.Expressions
{
    public class VariableModificationExpression : Expression
    {
        public string Variable { get; }

        public Expression Value { get; }


        public VariableModificationExpression(string variable, Expression value)
        {
            Variable = variable;
            Value = value;
        }

        public VariableModificationExpression(VariableExpression variable, Expression value)
            : this(variable.Name, value)
        {
        }
    }
}