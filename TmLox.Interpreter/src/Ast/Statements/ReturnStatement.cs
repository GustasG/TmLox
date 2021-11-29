namespace TmLox.Interpreter.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression? Value { get; }


        public ReturnStatement()
        {
            Value = null;
        }

        public ReturnStatement(Expression? value)
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