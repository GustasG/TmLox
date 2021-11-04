namespace TmLox.Ast.Statements
{
    public class VariableDeclarationStatement : Statement
    {
        public string Name { get; }

        public Expression? Value { get; }

        public VariableDeclarationStatement(string name, Expression? value)
        {
            Name = name;
            Value = value;
        }

        public VariableDeclarationStatement(string name)
            : this(name, null)
        {
        }

        public VariableDeclarationStatement(Token name, Expression? value)
            : this(name.Value as string, value)
        {
        }
    }
}
