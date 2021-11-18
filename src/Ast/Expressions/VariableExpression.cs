using System.Diagnostics;

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
        {
            Debug.Assert(token.TokenType == TokenType.Identifier && token.Value != null, "Token is not a valid identifier");

            Name = token.Value as string;
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