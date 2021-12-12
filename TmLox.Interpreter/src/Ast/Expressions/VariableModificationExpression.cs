namespace TmLox.Interpreter.Ast.Expressions;

public abstract class VariableModificationExpression : Expression
{
    public string Variable { get; }

    public Expression Value { get; }

    protected VariableModificationExpression(string variable, Expression value)
    {
        Variable = variable;
        Value = value;
    }
}