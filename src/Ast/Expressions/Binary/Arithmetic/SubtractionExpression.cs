﻿namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class SubtractionExpression : BinaryArithmeticExpression
    {
        public SubtractionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}