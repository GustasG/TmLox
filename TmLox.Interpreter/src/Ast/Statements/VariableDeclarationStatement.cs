namespace TmLox.Interpreter.Ast.Statements;

public class VariableDeclarationStatement : Statement
{
    public override NodeType Type => NodeType.VariableDeclaration;

    public string Name { get; }

    public Expression? Value { get; }

    public VariableDeclarationStatement(string name, Expression? value)
    {
        Name = name;
        Value = value;
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}