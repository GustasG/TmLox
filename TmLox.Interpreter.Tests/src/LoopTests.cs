namespace TmLox.Interpreter.Tests;

using NUnit.Framework;

public class LoopTests
{
    [Test]
    [TestCase(10)]
    [TestCase(50)]
    [TestCase(100)]
    public void Test_While_Loop_With_Simple_Variable_Comparision_Loop_Body_Should_Execute_Provided_Number_Of_Times(
        int repetitions)
    {
        var code = @"
                var i = 0;

                while (i < repetitions) {
                    i += 1;
                }
            ";

        var script = new Script();
        script["repetitions"] = AnyValue.CreateInteger(repetitions);
        script.RunString(code);

        Assert.AreEqual(repetitions, script["i"].AsInteger());
    }

    [Test]
    [TestCase(10)]
    [TestCase(50)]
    [TestCase(100)]
    public void Test_Infinite_While_Loop_With_Break_Statement_Inside_Loop_Should_Stop_When_Given_Number_Is_Reached(
        int repetitions)
    {
        var code = @"
                var i = 0;

                while (true) {
                    if (i == repetitions) {
                        break;
                    }

                    i += 1;
                }
            ";

        var script = new Script();
        script["repetitions"] = AnyValue.CreateInteger(repetitions);
        script.RunString(code);

        Assert.AreEqual(repetitions, script["i"].AsInteger());
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(3, 6)]
    [TestCase(4, 24)]
    [TestCase(5, 120)]
    [TestCase(6, 720)]
    [TestCase(7, 5040)]
    [TestCase(8, 40320)]
    [TestCase(9, 362880)]
    [TestCase(10, 3628800)]
    public void Test_For_Loop_That_Calculates_Factorial_Should_Produce_Valid_Given_Number_Factorial(int n, int expected)
    {
        var code = @"
                var result = 1;

                for (var i = 1; i <= n; i += 1) {
                    result *= i;
                }
            ";

        var script = new Script();
        script["n"] = AnyValue.CreateInteger(n);
        script.RunString(code);

        Assert.AreEqual(expected, script["result"].AsInteger());
    }

    [Test]
    [TestCase(10)]
    [TestCase(50)]
    [TestCase(100)]
    public void
        Test_For_Loop_That_Contains_No_Initial_Condition_And_Increment_Expression_Should_Produce_Infinite_Loop_That_Will_Stop_With_Break_Statement(
            int repetitions)
    {
        var code = @"
                var counter = 0;

                for(;;) {
                    if (counter == repetitions) {
                        break;
                    }

                    counter += 1;
                }
            ";

        var script = new Script();
        script["repetitions"] = AnyValue.CreateInteger(repetitions);
        script.RunString(code);

        Assert.AreEqual(repetitions, script["repetitions"].AsInteger());
    }
}