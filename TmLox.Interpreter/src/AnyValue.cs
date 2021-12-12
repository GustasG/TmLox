namespace TmLox.Interpreter;

using System;

using Errors;
using Execution.Functions;

public struct AnyValue : IEquatable<AnyValue>
{
    public object? Value { get; private init; }

    public AnyValueType Type { get; private init; }
    
#pragma warning disable CS8605, CS8603
    
    public bool IsNull()
    {
        return Type == AnyValueType.Null;
    }

    public bool IsBool()
    {
        return Type == AnyValueType.Bool;
    }

    public bool IsInteger()
    {
        return Type == AnyValueType.Integer;
    }

    public bool IsFloat()
    {
        return Type == AnyValueType.Float;
    }

    public bool IsNumber()
    {
        return IsInteger() || IsFloat();
    }

    public bool IsString()
    {
        return Type == AnyValueType.String;
    }

    public bool IsFunction()
    {
        return Type == AnyValueType.Function;
    }
    
    public bool AsBool()
    {
        return (bool)Value;
    }

    public long AsInteger()
    {
        return Type switch
        {
            AnyValueType.Integer => (long)Value,
            AnyValueType.Float => (long)(double)Value,
            _ => throw new ValueError($"Cannot convert {Type} to int"),
        };
    }

    public double AsFloat()
    {
        return Type switch
        {
            AnyValueType.Integer => (long)Value,
            AnyValueType.Float => (double)Value,
            _ => throw new ValueError($"Cannot convert {Type} to float"),
        };
    }

    public string AsString()
    {
        return Value as string;
    }

    public ICallable AsFunction()
    {
        return Value as ICallable;
    }

    public override string ToString()
    {
        return Value != null ? Value.ToString() : "null";
    }


    public static AnyValue CreateNull()
    {
        return new AnyValue
        {
            Value = null,
            Type = AnyValueType.Null
        };
    }

    public static AnyValue CreateBool(bool value)
    {
        return new AnyValue
        {
            Value = value,
            Type = AnyValueType.Bool
        };
    }

    public static AnyValue CreateInteger(long value)
    {
        return new AnyValue
        {
            Value = value,
            Type = AnyValueType.Integer
        };
    }

    public static AnyValue CreateFloat(double value)
    {
        return new AnyValue
        {
            Value = value,
            Type = AnyValueType.Float
        };
    }

    public static AnyValue CreateString(string value)
    {
        return new AnyValue
        {
            Value = value,
            Type = AnyValueType.String
        };
    }

    public static AnyValue CreateFunction(ICallable function)
    {
        return new AnyValue
        {
            Value = function,
            Type = AnyValueType.Function
        };
    }

    public bool Equals(AnyValue other)
    {
        return Equals(Value, other.Value);
    }
    
#pragma warning restore CS8605, C8603
}