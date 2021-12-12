namespace TmLox.Interpreter;

internal enum Lexeme
{
    OpComma, // ,
    OpSemicolon, // ;

    OplParen, // (
    OpRParen, // )
    OpLBrace, // {
    OprBrace, // }

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
    KwNull, // nil
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