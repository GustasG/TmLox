namespace TmLox.Interpreter.Ast.Expressions.Variable
{
    public class VariableSubtractionExpression : VariableModificationExpression
    {
        public override NodeType Type
        {
            get => NodeType.VariableSubtraction;
        }


        public VariableSubtractionExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}