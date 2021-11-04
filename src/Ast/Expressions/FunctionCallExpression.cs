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
            : this(name.Value as string, arguments)
        {
        }
    }
}