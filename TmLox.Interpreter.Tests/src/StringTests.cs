namespace TmLox.Interpreter.Tests;

using NUnit.Framework;

using Errors;

public class StringTests
{
    [Test]
    public void Test_Creating_String_Without_Termination_Character_Should_Produce_Error_About_Unterminated_String()
    {
        var code = @"
                var test_variable = ""Hello world;
            ";

        var script = new Script();

        var ex = Assert.Throws<SyntaxError>(delegate { script.RunString(code); });

        StringAssert.Contains("Unterminated string", ex.Message);
    }

    [Test]
    public void Test_Creating_String_With_All_Valid_Escape_Symbols_Should_Produce_Valid_String()
    {
        var code = @"
                var test_variable = ""\' \"" \\ \a \b \f \n \r \t \v"";
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsString());
    }

    [Test]
    public void Test_Creating_String_With_Invalid_Escape_Symbols_Should_Produce_Error_About_Invalid_Escape_Symbol()
    {
        var code = @"
                var test_variable = ""\m"";
            ";

        var script = new Script();
        var ex = Assert.Throws<SyntaxError>(delegate { script.RunString(code); });

        StringAssert.Contains("Invalid escape symbol", ex.Message);
    }

    [Test]
    public void Test_Creating_String_With_Minus_Sign_Before_Literal_Should_Produce_Error_About_Unsupported_Operation()
    {
        var code = @"
                var value = -""First"";
            ";

        var script = new Script();
        var ex = Assert.Throws<ValueError>(delegate { script.RunString(code); });

        StringAssert.Contains("Unary - not supported for type String", ex.Message);
    }
}