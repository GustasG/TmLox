namespace TmLox.Ast.Expressions
{
    public class VariableExpression : Expression
    {
        public string Name { get; }


        public VariableExpression(string name)
        {
            Name = name;
        }

        public VariableExpression(Token token)
            : this(token.Value as string)
        {
        }
    }
}