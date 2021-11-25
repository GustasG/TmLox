using TmLox.Errors;
using TmLox.Interpreter.Functions;

namespace TmLox
{
    public enum AnyValueType
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

        public IFunction AsFunction()
        {
            return Value as IFunction;
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

        public static AnyValue CreateFunction(IFunction function)
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
    }
}
