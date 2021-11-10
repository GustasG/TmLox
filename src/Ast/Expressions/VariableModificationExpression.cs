using System.Diagnostics;

namespace TmLox.Ast.Expressions
{
    public abstract class VariableModificationExpression : Expression
    {
        public string Variable { get; }

        public Expression Value { get; }


        public VariableModificationExpression(string variable, Expression value)
        {
            Variable = variable;
            Value = value;
        }

        public VariableModificationExpression(Token variable, Expression value)
        {
            Debug.Assert(variable.TokenType == TokenType.Identifier && variable.Value != null, "Token is not a valid identifier");

            Variable = variable.Value as string;
            Value = value;
        }
    }
}