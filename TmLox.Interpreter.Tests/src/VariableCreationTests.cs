namespace TmLox.Interpreter.Tests;

using NUnit.Framework;

using Errors;

public class VariableCreationTests
{
    [Test]
    public void Test_Variable_Creation_Without_Explicitly_Assigning_Value_Should_Produce_Null_Valued_Value()
    {
        const string code = @"
                var test_variable;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsNull());
    }

    [Test]
    public void Test_Variable_Creation_With_Null_Literal_Should_Produce_Null_Valued_Value()
    {
        const string code = @"
                var test_variable = null;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsNull());
    }

    [Test]
    public void Test_Variable_Creation_With_False_Bool_Literal_Should_Produce_Boolean_Valued_Value_With_Value_As_False()
    {
        const string code = @"
                var test_variable = false;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.False(testVariable.AsBool());
    }

    [Test]
    public void Test_Variable_Creation_With_True_Bool_Literal_Should_Produce_Boolean_Valued_Value_With_Value_As_True()
    {
        const string code = @"
                var test_variable = true;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsBool());
        Assert.True(testVariable.AsBool());
    }

    [Test]
    public void Test_Variable_Creation_With_Integer_Literal_Should_Produce_Integer_Valued_Value_With_Value_As_42()
    {
        const string code = @"
                var test_variable = 42;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsInteger());
        Assert.AreEqual(42, testVariable.AsInteger());
    }

    [Test]
    public void Test_Variable_Creation_With_Float_Literal_Should_Produce_Given_Float_Valued_Value()
    {
        const string code = @"
                var test_variable = -567.357;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(-567.357, testVariable.AsFloat(), 0.1);
    }

    [Test]
    public void Test_Variable_Creation_With_String_Literal_Should_Produce_Given_String_Valued_Value()
    {
        const string code = @"
                var test_variable = ""Hello world"";
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsString());
        Assert.AreEqual("Hello world", testVariable.AsString());
    }

    [Test]
    public void
        Test_Variable_Creation_With_Starting_Character_As_Underscore_And_Value_As_Integer_Literal_Should_Produce_Given_Integer_Value()
    {
        const string code = @"
                var _tmp = -537;
            ";

        var script = new Script();
        script.RunString(code);

        var tmpVariable = script["_tmp"];

        Assert.True(tmpVariable.IsInteger());
        Assert.AreEqual(-537, tmpVariable.AsInteger());
    }

    [Test]
    public void
        Test_Variable_Creation_With_Incorrectly_Formatted_Float_Literal_Should_Produce_Error_About_Invalid_Number()
    {
        const string code = @"
                var number = 12.45.787.72;
            ";

        var script = new Script();
        var ex = Assert.Throws<SyntaxError>(delegate { script.RunString(code); });

        StringAssert.Contains("Invalid number", ex?.Message);
    }
}