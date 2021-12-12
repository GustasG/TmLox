﻿namespace TmLox.Interpreter.Execution;

using System.Collections.Generic;

public class Environment
{
    private readonly Environment? _enclosing;

    private readonly IDictionary<string, AnyValue> _values;

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
        
        return _enclosing != null && _enclosing.Assign(name, value);
    }

    public bool TryGet(string name, out AnyValue value)
    {
        if (_values.TryGetValue(name, out value))
        {
            return true;
        }
        if (_enclosing != null)
        {
            return _enclosing.TryGet(name, out value);
        }

        value = default;
        return false;
    }
}