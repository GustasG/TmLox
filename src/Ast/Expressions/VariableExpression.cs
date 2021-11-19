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
            : this(token.Value.AsString())
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Variable;
        }
    }
}