namespace TmLox.Interpreter.Ast.Expressions.Variable
{
    public class VariableModulusExpression : VariableModificationExpression
    {
        public override NodeType Type
        {
            get => NodeType.VariableModulus;
        }


        public VariableModulusExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}