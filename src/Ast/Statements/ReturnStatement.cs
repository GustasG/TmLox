namespace TmLox.Ast.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression? Value { get; }


        public ReturnStatement()
        {
            Value = null;
        }

        public ReturnStatement(Expression value)
        {
            Value = value;
        }
    }
}