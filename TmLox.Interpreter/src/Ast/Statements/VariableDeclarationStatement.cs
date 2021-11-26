namespace TmLox.Ast.Statements
{
    public class VariableDeclarationStatement : Statement
    {
        public string Name { get; }

        public Expression? Value { get; }

        public VariableDeclarationStatement(string name, Expression? value = null)
        {
            Name = name;
            Value = value;
        }

        public VariableDeclarationStatement(Token name, Expression? value = null)
            : this(name.Value.AsString(), value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.VariableDeclaration;
        }
    }
}
