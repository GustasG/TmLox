using System.Collections.Generic;

namespace TmLox.Interpreter.Ast.Expressions
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

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.FunctionCall;
        }
    }
}