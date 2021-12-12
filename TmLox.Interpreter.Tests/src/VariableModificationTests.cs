namespace TmLox.Interpreter.Tests;

using NUnit.Framework;

public class VariableModificationTests
{
    [Test]
    public void Test_Variable_Reassignment_Modification_From_Integer_To_String_Should_Produce_Valid_String()
    {
        const string code = @"
                var test_variable = 50;
                test_variable = ""Hello"";
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsString());
        Assert.AreEqual("Hello", testVariable.AsString());
    }

    [Test]
    public void Test_Variable_Addition_Modification_With_Both_Arguments_As_Integers_Should_Produce_Valid_Integer()
    {
        const string code = @"
                var test_variable = 50;
                test_variable += 20;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsInteger());
        Assert.AreEqual(70, testVariable.AsInteger());
    }


    [Test]
    public void
        Test_Variable_Addition_Modification_With_First_Argument_As_Integer_And_Second_Argument_As_Float_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 50;
                test_variable += 13.2;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(63.2, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void
        Test_Variable_Addition_Modification_With_First_Argument_As_Float_And_Second_Argument_As_Integer_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 16.6;
                test_variable += 4;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(20.6, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Addition_Modification_With_Both_Arguments_As_Floats_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 17.7;
                test_variable += 12.3;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(30.0, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Subtraction_Modification_With_Both_Arguments_As_Integers_Should_Produce_Valid_Integer()
    {
        const string code = @"
                var test_variable = 50;
                test_variable -= 20;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsInteger());
        Assert.AreEqual(30, testVariable.AsInteger());
    }

    [Test]
    public void
        Test_Variable_Subtraction_Modification_With_First_Argument_As_Integer_And_Second_Argument_As_Float_Should_Produce_Valid_float()
    {
        const string code = @"
                var test_variable = 50;
                test_variable -= 5.5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(44.5, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void
        Test_Variable_Subtraction_Modification_With_First_Argument_As_Float_And_Second_Argument_As_Integer_Should_Produce_Valid_float()
    {
        const string code = @"
                var test_variable = 17.5;
                test_variable -= 7;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(10.5, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Subtraction_Modification_With_Both_Arguments_As_floats_Should_Produce_Valid_float()
    {
        const string code = @"
                var test_variable = 15.5;
                test_variable -= 5.5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(10.0, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Multiplication_Modification_With_Both_Arguments_As_Integers_Should_Produce_Valid_Integer()
    {
        const string code = @"
                var test_variable = 50;
                test_variable *= 2;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsInteger());
        Assert.AreEqual(100, testVariable.AsInteger());
    }

    [Test]
    public void
        Test_Variable_Multiplication_Modification_With_First_Argument_As_Integer_And_Second_Argument_As_Float_Should_Produce_Valid_float()
    {
        const string code = @"
                var test_variable = 25;
                test_variable *= 2.3;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(57.5, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void
        Test_Variable_Multiplication_Modification_With_First_Argument_As_Float_And_Second_Argument_As_Integer_Should_Produce_Valid_float()
    {
        const string code = @"
                var test_variable = 7.35;
                test_variable *= 5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(36.75, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Multiplication_Modification_With_Both_Arguments_As_Floats_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 15.5;
                test_variable *= 3.8;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(58.9, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Division_Modification_With_Both_Arguments_As_Integers_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 50;
                test_variable /= 20;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(2.5, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void
        Test_Variable_Division_Modification_With_First_Argument_As_Integer_And_Second_Argument_As_Float_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 100;
                test_variable /= 5.5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(18.18, testVariable.AsFloat(), 0.05);
    }


    [Test]
    public void
        Test_Variable_Division_Modification_With_First_Argument_As_Float_And_Second_Argument_As_Integer_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 275.15;
                test_variable /= 5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(55.03, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Division_Modification_With_Both_Arguments_As_Floats_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 275.15;
                test_variable /= 5.5;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(50.027, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Modulus_Modification_With_Both_Arguments_As_Integers_Should_Produce_Valid_Integer()
    {
        const string code = @"
                var test_variable = 50;
                test_variable %= 20;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsInteger());
        Assert.AreEqual(10, testVariable.AsInteger());
    }

    [Test]
    public void
        Test_Variable_Modulus_Modification_With_First_Argument_As_Integer_And_Second_Argument_As_Float_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 100;
                test_variable %= 7.7;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(7.6, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void
        Test_Variable_Modulus_Modification_With_First_Argument_As_Float_And_Second_Argument_As_Integer_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 203.15;
                test_variable %= 8;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(3.15, testVariable.AsFloat(), 0.05);
    }

    [Test]
    public void Test_Variable_Modulus_Modification_With_Both_Arguments_As_Floats_Should_Produce_Valid_Float()
    {
        const string code = @"
                var test_variable = 57.3;
                test_variable %= 10.4;
            ";

        var script = new Script();
        script.RunString(code);

        var testVariable = script["test_variable"];

        Assert.True(testVariable.IsFloat());
        Assert.AreEqual(5.3, testVariable.AsFloat(), 0.1);
    }
}