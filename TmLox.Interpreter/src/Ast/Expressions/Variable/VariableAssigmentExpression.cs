namespace TmLox.Interpreter.Ast.Expressions.Variable
{
    public class VariableAssigmentExpression : VariableModificationExpression
    {
        public VariableAssigmentExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.VariableAssigment;
        }
    }
}