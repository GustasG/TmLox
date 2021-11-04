namespace TmLox
{
    public enum TokenType
    {
        OpComma, // ,
        OpDot, // .
        OpQuestion, // ?
        OpColon, // :
        OpSemicolon, // ;

        OPLParen, // (
        OpRParen, // )
        OpLBrace, // {
        OPRBrace, // }
        OpLBracket, // [
        OpRBracket, // ]

        OpPlus, // +
        OpPlusEq, // +=
        OpMinus, // -
        OpMinusEq, // -=
        OpMul, // *
        OpMulEq, // *=
        OpDiv, // /
        OpDivEq, // /=
        OpMod, // %
        OpModEq, // %=
        OpAssign, // =
        OpEq, // ==
        OpExclamation, // !
        OpNotEqual, // !=

        OpLess, // <
        OpLessEq, // <=
        OpMore, // >
        OpMoreEq, // >=

        KwAnd, // and
        KwBreak, // break
        KwClass, // class
        KwElse, // else
        KwFalse, // false
        KwTrue, // true
        KwFor, // for
        KwFun, // fun
        KwIf, // if
        KwNil, // nil
        KwOr, // or
        KwReturn, // return
        KwSuper, // super
        KwThis, // this
        KwVar, // var
        KwWhile, // while

        LitInt,
        LitFloat,
        LitString,

        Identifier,

        Eof
    }
}