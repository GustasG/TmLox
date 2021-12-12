namespace TmLox.Interpreter.Tests;

using NUnit.Framework;

using Errors;

public class ComparisonTests
{
    [Test]
    public void Test_Equality_Between_Nulls_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = null == null;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Equality_Between_Integers_Should_Produce_False_Value()
    {
        const string code = @"
                var test_variable = 5 == 6;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_Inequality_Between_Integers_Should_Produce_False_Value()
    {
        const string code = @"
                var test_variable = 5 != 5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_Less_Comparison_Between_Two_Integers_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = 5 < 6;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Less_Comparison_Between_Two_Floats_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = 5.42 < 6.17;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Less_Comparison_Between_Number_And_String_Should_Produce_Error_About_Incomparable_Types()
    {
        const string code = @"
                var test_variable = 5.42 < ""Hello"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }

    [Test]
    public void Test_Less_Equal_Expression_Between_Two_Integers_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = 6 <= 6;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Less_Equal_Comparison_Between_Number_And_String_Should_Produce_Error_About_Incomparable_Types()
    {
        const string code = @"
                var test_variable = 5 <= ""World"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }

    [Test]
    public void Test_More_Comparison_Between_Two_Numbers_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = 7 > 6;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_More_Comparison_Between_Number_And_String_Should_Produce_Error_About_Incomparable_Types()
    {
        const string code = @"
                var test_variable = 5 <= ""First"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }

    [Test]
    public void Test_More_Equal_Comparison_Between_Two_Numbers_Should_Produce_True_Value()
    {
        const string code = @"
                var test_variable = 7 >= 7;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_More_Equal_Comparison_Between_Number_And_String_Should_Produce_Error_About_Incomparable_Types()
    {
        const string code = @"
                var test_variable = 5 >= ""Second"";
            ";

        var script = new Script();

        Assert.Throws<ValueError>(delegate { script.RunString(code); });
    }
}