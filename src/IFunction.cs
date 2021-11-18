using System.Collections.Generic;

namespace TmLox
{
    public interface IFunction
    {
        AnyValue Call(IEnumerable<AnyValue> arguments);
    }
}
