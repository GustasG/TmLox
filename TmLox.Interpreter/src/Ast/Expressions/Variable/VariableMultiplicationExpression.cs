namespace TmLox.Interpreter.Ast.Expressions.Variable;

public class VariableMultiplicationExpression : VariableModificationExpression
{
    public override NodeType Type => NodeType.VariableMultiplication;

    public VariableMultiplicationExpression(string variable, Expression value)
        : base(variable, value)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}