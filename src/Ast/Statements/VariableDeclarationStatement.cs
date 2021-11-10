using System.Diagnostics;

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
        {
            Debug.Assert(name.TokenType == TokenType.Identifier && name.Value != null, "Token is not a valid identifier");

            Name = name.Value as string;
            Value = value;
        }

        public override NodeType Type()
        {
            return NodeType.VariableDeclaration;
        }
    }
}
