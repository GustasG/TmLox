namespace TmLox
{
    public enum Lexeme
    {
        OpComma, // ,
        OpSemicolon, // ;

        OPLParen, // (
        OpRParen, // )
        OpLBrace, // {
        OPRBrace, // }

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
        KwElif, // elif
        KwElse, // else
        KwFalse, // false
        KwTrue, // true
        KwFor, // for
        KwFun, // fun
        KwIf, // if
        KwNil, // nil
        KwOr, // or
        KwReturn, // return
        KwVar, // var
        KwWhile, // while

        LitInt,
        LitFloat,
        LitString,

        Identifier,

        Eof
    }
}