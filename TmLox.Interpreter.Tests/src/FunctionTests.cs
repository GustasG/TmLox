namespace TmLox.Interpreter.Tests;

using System.Collections.Generic;

using NUnit.Framework;

using Errors;

public class FunctionTests
{
    private Script _script;

    [SetUp]
    public void SetUpScript()
    {
        var code = @"
                var number = 53;

                fun foo() {
                    return 42;
                }
            ";

        _script = new Script();
        _script.RunString(code);
    }

    [Test]
    public void Test_Calling_Lox_Function_From_Native_Code_Should_Produce_Valid_Returned_Integer_Value()
    {
        var foo = _script["foo"].AsFunction();
        var result = foo.Call(_script.Interpreter, new List<AnyValue>());

        Assert.True(result.IsInteger());
        Assert.AreEqual(42, result.AsInteger());
    }

    [Test]
    public void Test_Calling_Lox_Function_Inside_Interpreter_Should_Produce_Valid_Returned_Integer_Value()
    {
        const string fooResultCode = @"
                var foo_result = foo();
            ";

        _script.RunString(fooResultCode);
        var fooResult = _script["foo_result"];

        Assert.True(fooResult.IsInteger());
        Assert.AreEqual(42, fooResult.AsInteger());
    }

    [Test]
    public void
        Test_Calling_Lox_Function_With_Incorrect_Number_Of_Arguments_Should_Produce_Error_About_Expected_Argument_Count()
    {
        const string incorrectFooResult = @"
                var tmp = foo(1, 2, 3);
            ";

        var ex = Assert.Throws<ValueError>(delegate { _script.RunString(incorrectFooResult); });

        StringAssert.Contains("Function foo expects 0 arguments, while 3 arguments were provided", ex?.Message);
    }

    [Test]
    public void Test_Calling_Not_Existent_Function_Should_Produce_Error_Saying_That_Function_Does_Not_Exist()
    {
        const string code = @"
                goo();
            ";

        var ex = Assert.Throws<ValueError>(delegate { _script.RunString(code); });

        StringAssert.Contains("Function goo does not exist", ex?.Message);
    }

    [Test]
    public void Test_Calling_Number_Should_Produce_Error_Saying_That_Given_Variable_Is_Not_A_Function()
    {
        const string code = @"
                number();
            ";

        var ex = Assert.Throws<ValueError>(delegate { _script.RunString(code); });

        StringAssert.Contains("number is instance of Integer and not a function", ex?.Message);
    }
}