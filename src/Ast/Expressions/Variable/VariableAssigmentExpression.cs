namespace TmLox.Ast.Expressions.Variable
{
    public class VariableAssigmentExpression : VariableModificationExpression
    {
        public VariableAssigmentExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableAssigmentExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }

        public override NodeType Type()
        {
            return NodeType.VariableAssigment;
        }
    }
}