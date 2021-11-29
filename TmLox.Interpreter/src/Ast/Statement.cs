namespace TmLox.Interpreter.Ast
{
    public abstract class Statement
    {
        public abstract T Accept<T>(IVisitor<T> visitor);

        public abstract NodeType Type();
    }
}