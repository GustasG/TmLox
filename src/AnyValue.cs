using System;

using TmLox.Errors;
using TmLox.Interpreter.Functions;

namespace TmLox
{
    [Flags]
    public enum AnyValueType
    {
        Null = 0,
        Bool = 1,
        Integer = 2,
        Float = 4,
        String = 8,
        Function = 16
    }


    public struct AnyValue
    {
        public object? Value { get; private set; }

        public AnyValueType Type { get; private set; }


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
                AnyValueType.Integer => (double)(long)Value,
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

        public static bool operator== (AnyValue lhs, AnyValue rhs)
        {
            return Equals(lhs.Value, rhs.Value);
        }

        public static bool operator != (AnyValue lhs, AnyValue rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object? obj)
        {
            return Equals(Value, obj);
        }

        public override int GetHashCode()
        {
            if (Value != null)
                return Type.GetHashCode() ^ Value.GetHashCode();
            
            return Type.GetHashCode();
        }
    }
}
