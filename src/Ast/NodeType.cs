namespace TmLox.Ast
{
    public enum NodeType
    {
        // Arithmetic expressions
        Addition,
        Division,
        Modulus,
        Multiplication,
        Subtraction,

        // Logical expressions
        And,
        Equal,
        LessEqual,
        Less,
        MoreEqual,
        More,
        NotEqual,
        Or,

        // Literal expressions
        BoolLiteral,
        FloatLiteral,
        IntLiteral,
        NullLiteral,
        StringLiteral,

        // Unary expressions
        UnaryMinus,
        UnaryNot,

        // Variable expressions
        VariableAddition,
        VariableAssigment,
        VariableDivision,
        VariableModulus,
        VariableMultiplication,
        VariableSubtraction,

        // Other expressions
        FunctionCall,
        Variable,

        // Statements
        Break,
        For,
        FunctionDeclaration,
        If,
        Elif,
        Return,
        VariableDeclaration,
        While
    }
}
