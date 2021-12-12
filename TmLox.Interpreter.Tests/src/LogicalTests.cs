namespace TmLox.Interpreter.Tests;

using NUnit.Framework;

using Errors;

public class LogicalTests
{
    [Test]
    public void Test_Or_Condition_With_Both_Arguments_As_False_Should_Produce_False_Value()
    {
        const string code = @"
                var test_variable = false or false;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_Or_Condition_With_First_Argument_As_False_And_Second_Argument_As_True_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = false or true;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Or_Condition_With_First_Argument_As_True_And_Second_Argument_As_False_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = true or false;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Or_Condition_With_Both_Arguments_As_True_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = true or true;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void
        Test_Or_Condition_With_First_Argument_As_False_And_Second_As_Integer_Should_Produce_Error_That_String_Cannot_Be_Converted_To_Bool()
    {
        const string code = @"
                var test_variable = false or ""String"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }

    [Test]
    public void Test_And_Condition_With_Both_Arguments_As_False_Should_Produce_False_Value()
    {
        const string code = @"
                var test_variable = false and false;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_And_Condition_With_First_Argument_As_False_And_Second_Argument_As_True_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = false and true;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_And_Condition_With_First_Argument_As_True_And_Second_Argument_As_False_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = true and false;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_And_Condition_With_Both_Arguments_As_True_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = true and true;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void
        Test_And_Condition_With_First_Argument_As_True_And_Second_Argument_As_String_Value_Should_Produce_Error_That_String_Cannot_Be_Converted_To_Bool()
    {
        const string code = @"
                var test_variable = true and ""String"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }

    [Test]
    public void Test_Negation_Condition_With_False_Argument_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = !false;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Negation_Condition_With_True_Argument_Should_Produce_False_Value()
    {
        const string code = @"
                var test_variable = !true;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_Negation_With_Value_That_Is_Not_Boolean_Should_Produce_Error_About_Unsupported_Operation()
    {
        const string code = @"
                var tmp = !""First"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }
}