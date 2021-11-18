using System.Collections.Generic;

using TmLox.Errors;

namespace TmLox.Interpreter
{
    public class Environment
    {
        private Environment? _enclosing;

        private IDictionary<string, AnyValue> _values;


        public Environment()
        {
            _enclosing = null;
            _values = new Dictionary<string, AnyValue>();
        }

        public Environment(Environment enclosing)
        {
            _enclosing = enclosing;
            _values = new Dictionary<string, AnyValue>();
        }

        public void Add(string name, AnyValue value)
        {
            _values[name] = value;
        }

        public bool Assign(string name, AnyValue value)
        {
            if (_values.ContainsKey(name))
            {
                _values[name] = value;
                return true;
            }
            else if (_enclosing != null)
            {
                return _enclosing.Assign(name, value);
            }

            return false;
        }

        public bool TryGet(string name, out AnyValue value)
        {
            if (_values.TryGetValue(name, out var existingValue))
            {
                value = existingValue;
                return true;
            }

            if (_enclosing != null)
                return _enclosing.TryGet(name, out value);

            value = default;
            return false;
        }
    }
}
