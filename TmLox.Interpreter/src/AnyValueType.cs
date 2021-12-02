using System;

namespace TmLox.Interpreter
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
}
