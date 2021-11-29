namespace TmLox.Interpreter.Ast.Expressions
{
    public class VariableExpression : Expression
    {
        public string Name { get; }


        public VariableExpression(string name)
        {
            Name = name;
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