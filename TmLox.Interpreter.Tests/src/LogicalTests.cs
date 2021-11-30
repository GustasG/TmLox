using NUnit.Framework;
using TmLox.Interpreter.Errors;

namespace TmLox.Interpreter.Tests
{
    public class LogicalTests
    {
        [Test]
        public void Test_Or_Condition_With_Both_Arguments_As_False_Should_Produce_False_Value()
        {
            var code = @"
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
            var code = @"
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
            var code = @"
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
            var code = @"
                var test_variable = true or true;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsBool());
            Assert.True(testVariable.AsBool());
        }

        [Test]
        public void Test_And_Condition_With_Both_Arguments_As_False_Should_Produce_False_Value()
        {
            var code = @"
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
            var code = @"
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
            var code = @"
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
            var code = @"
                var test_variable = true and true;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsBool());
            Assert.True(testVariable.AsBool());
        }

        [Test]
        public void Test_Negation_Condition_With_False_Argument_Should_Produce_True_Value()
        {
            var code = @"
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
            var code = @"
                var test_variable = !true;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsBool());
            Assert.False(testVariable.AsBool());
        }

        [Test]
        public void Test_Negation_With_Value_That_Is_Not_Boolean_Should_Produce_Error_Abount_Unsupported_Operation()
        {
            var code = @"
                var tmp = !""First"";
            ";

            var script = new Script();
            var ex = Assert.Throws<ValueError>(delegate
            {
                script.RunString(code);
            });

            StringAssert.Contains("Unary ! not supported", ex.Message);
        }
    }
}