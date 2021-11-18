using System;

using TmLox.Errors;

namespace TmLox
{
    public enum ValueType
    {
        Null,
        Bool,
        Integer,
        Float,
        String,
        Function
    }


    public struct AnyValue
    {
        public object? Value { get; private set; }

        public ValueType Type { get; private set; }

        public bool IsNull()
        {
            return Type == ValueType.Null;
        }

        public bool IsBool()
        {
            return Type == ValueType.Bool;
        }

        public bool IsInteger()
        {
            return Type == ValueType.Integer;
        }

        public bool IsFloat()
        {
            return Type == ValueType.Float;
        }

        public bool IsNumber()
        {
            return IsInteger() || IsFloat();
        }

        public bool IsString()
        {
            return Type == ValueType.String;
        }

        public bool IsFunction()
        {
            return Type == ValueType.Function;
        }

        public bool IsPrimitive()
        {
            return IsNull() || IsBool() || IsInteger() || IsFloat() || IsString();
        }

        public bool AsBool()
        {
            return (bool)Value;
        }

        public long AsInteger()
        {
            return Type switch
            {
                ValueType.Integer => (long)Value,
                ValueType.Float => (long)(double)Value,
                _ => throw new ValueError($"Cannot convert {Type} to int"),
            };
        }

        public double AsFloat()
        {
            return Type switch
            {
                ValueType.Integer => (double)(long)Value,
                ValueType.Float => (double)Value,
                _ => throw new ValueError($"Cannot convert {Type} to float"),
            };
        }

        public string AsString()
        {
            return Value as string;
        }

        public IFunction AsFunction()
        {
            return Value as IFunction;
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : "null";
        }

        public static AnyValue FromNull()
        {
            return new AnyValue
            {
                Value = null,
                Type = ValueType.Null
            };
        }

        public static AnyValue FromBool(bool value)
        {
            return new AnyValue
            {
                Value = value,
                Type = ValueType.Bool
            };
        }

        public static AnyValue FromInteger(long value)
        {
            return new AnyValue
            {
                Value = value,
                Type = ValueType.Integer
            };
        }

        public static AnyValue FromFloat(double value)
        {
            return new AnyValue
            {
                Value = value,
                Type = ValueType.Float
            };
        }

        public static AnyValue FromString(string value)
        {
            return new AnyValue
            {
                Value = value,
                Type = ValueType.String
            };
        }

        public static AnyValue FromCallable(IFunction callable)
        {
            return new AnyValue
            {
                Value = callable,
                Type = ValueType.Function
            };
        }
    }
}
