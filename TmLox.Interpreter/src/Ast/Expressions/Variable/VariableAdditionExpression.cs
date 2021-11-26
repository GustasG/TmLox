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

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.VariableAddition;
        }
    }
}