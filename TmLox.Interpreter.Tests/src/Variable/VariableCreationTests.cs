using NUnit.Framework;

namespace TmLox.Interpreter.Tests.Variable
{
    public class VariableCreationTests
    {
        [Test]
        public void TestVariableCreationWIthDefaultEmptyValue()
        {
            var code = @"
                var test_variable;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsNull());
        }

        [Test]
        public void TestVariableCreationWithNullLiteral()
        {
            var code = @"
                var test_variable = null;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsNull());
        }

        [Test]
        public void TestVariableCreationWithFalseBoolLiteral()
        {
            var code = @"
                var test_variable = false;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsBool());
            Assert.False(testVariable.AsBool());
        }

        [Test]
        public void TestVariableCreationWithTrueBoolLiteral()
        {
            var code = @"
                var test_variable = true;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsBool());
            Assert.True(testVariable.AsBool());
        }

        [Test]
        public void TestVariableCreationWithIntegerLiteral()
        {
            var code = @"
                var test_variable = 42;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsInteger());
            Assert.AreEqual(42, testVariable.AsInteger());
        }

        [Test]
        public void TestVariableCreationWithFloatLiteral()
        {
            var code = @"
                var test_variable = -567.357;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsFloat());
            Assert.AreEqual(-567.357, testVariable.AsFloat(), 0.1);
        }

        [Test]
        public void TestVariableCreationWithStringLiteral()
        {
            var code = @"
                var test_variable = ""Hello world"";
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.True(testVariable.IsString());
            Assert.AreEqual("Hello world", testVariable.AsString());
        }
    }
}