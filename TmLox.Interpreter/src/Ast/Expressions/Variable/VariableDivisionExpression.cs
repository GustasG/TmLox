namespace TmLox.Interpreter.Ast.Expressions.Variable;

public class VariableDivisionExpression : VariableModificationExpression
{
    public override NodeType Type => NodeType.VariableDivision;

    public VariableDivisionExpression(string variable, Expression value)
        : base(variable, value)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}