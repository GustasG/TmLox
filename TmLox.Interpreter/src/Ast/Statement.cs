namespace TmLox.Interpreter.Ast;

public abstract class Statement
{
    public abstract NodeType Type { get; }

    public abstract T Accept<T>(IVisitor<T> visitor);
}