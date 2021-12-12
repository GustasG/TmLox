namespace TmLox.Interpreter.Ast.Expressions.Variable;

public class VariableAdditionExpression : VariableModificationExpression
{
    public override NodeType Type => NodeType.VariableAddition;

    public VariableAdditionExpression(string variable, Expression value)
        : base(variable, value)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}