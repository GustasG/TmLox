using System.Diagnostics;
using System.Collections.Generic;

namespace TmLox.Ast.Expressions
{
    public class FunctionCallExpression : Expression
    {
        public string Name { get; }

        public IList<Expression> Arguments { get; }


        public FunctionCallExpression(string name, IList<Expression> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public FunctionCallExpression(Token name, IList<Expression> arguments)
        {
            Debug.Assert(name.TokenType == TokenType.Identifier && name.Value != null, "Token is not a valid identifier");

            Name = name.Value as string;
            Arguments = arguments;
        }

        public override NodeType Type()
        {
            return NodeType.FunctionCall;
        }
    }
}