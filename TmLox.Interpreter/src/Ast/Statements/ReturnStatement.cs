namespace TmLox.Interpreter.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression? Value { get; }


        public ReturnStatement(Expression? value = null)
        {
            Value = value;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.Return;
        }
    }
}