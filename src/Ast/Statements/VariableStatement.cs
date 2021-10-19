namespace TmLox.Ast.Statements
{
    public class VariableStatement : Statement
    {
        public string Name { get; }

        public Expression? Value { get; }


        public VariableStatement(string name, Expression? value)
        {
            Name = name;
            Value = value;
        }

        public VariableStatement(Token name, Expression? value)
            : this(name.Value as string, value)
        {
        }
    }
}
