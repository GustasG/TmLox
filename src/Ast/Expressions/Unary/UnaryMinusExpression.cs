﻿namespace TmLox.Ast.Expressions.Unary
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public UnaryMinusExpression(Expression expression)
            : base(expression)
        {
        }

        public override NodeType Type()
        {
            return NodeType.UnaryMinus;
        }
    }
}