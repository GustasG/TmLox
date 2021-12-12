namespace TmLox.Interpreter.Execution.Functions;

using System.Collections.Generic;

using Ast;
using Ast.Statements;
using StackUnwinding;

internal class LoxFunction : ICallable
{
    public string Name { get; }

    private readonly Environment _environment;

    private readonly IList<string> _parameters;

    private readonly IList<Statement> _body;

    public LoxFunction(Environment environment, string name, IList<string> parameters, IList<Statement> body)
    {
        Name = name;
        _environment = environment;
        _parameters = parameters;
        _body = body;
    }

    public LoxFunction(Environment environment, FunctionDeclarationStatement functionDeclarationStatement)
        : this(environment, functionDeclarationStatement.Name, functionDeclarationStatement.Parameters, functionDeclarationStatement.Body)
    {
    }

    public bool CheckArity()
    {
        return true;
    }

    public int Arity()
    {
        return _parameters.Count;
    }

    public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
    {
        var environment = new Environment(_environment);

        for (int i = 0; i < _parameters.Count; i++)
        {
            environment.Add(_parameters[i], arguments[i]);
        }

        var currentEnvironment = interpreter.Environment;
        interpreter.Environment = environment;

        try
        {
            interpreter.Execute(_body);
        }
        catch (ReturnUnwind returnValue)
        {
            return returnValue.Value;
        }
        finally
        {
            interpreter.Environment = currentEnvironment;
        }
        
        return AnyValue.CreateNull();
    }

    public override string ToString()
    {
        return $"<function {Name}>";
    }
}