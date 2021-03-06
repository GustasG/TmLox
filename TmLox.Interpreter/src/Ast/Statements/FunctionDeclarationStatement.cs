namespace TmLox.Interpreter.Ast.Statements;

using System.Collections.Generic;

public class FunctionDeclarationStatement : Statement
{
    public override NodeType Type => NodeType.FunctionDeclaration;

    public string Name { get; }

    public IList<string> Parameters { get; }

    public IList<Statement> Body { get; }

    public FunctionDeclarationStatement(string name, IList<string> parameters, IList<Statement> body)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}