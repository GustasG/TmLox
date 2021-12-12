namespace TmLox.Interpreter.Ast.Expressions;

public class VariableExpression : Expression
{
    public override NodeType Type => NodeType.Variable;

    public string Name { get; }

    public VariableExpression(string name)
    {
        Name = name;
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}