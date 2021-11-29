using NUnit.Framework;

namespace TmLox.Interpreter.Tests
{
    public class VariableTest
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

            Assert.AreEqual(testVariable.Type, AnyValueType.Null);
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

            Assert.AreEqual(testVariable.Type, AnyValueType.Null);
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

            Assert.AreEqual(testVariable.Type, AnyValueType.Bool);
            Assert.AreEqual(testVariable.AsBool(), false);
        }

        [Test]
        public void TestVariableCreationWithTrueBoolLiteral()
        {
            var code = @"
                var test_variable = false;
            ";

            var script = new Script();
            script.RunString(code);

            var testVariable = script["test_variable"];

            Assert.AreEqual(testVariable.Type, AnyValueType.Bool);
            Assert.AreEqual(testVariable.AsBool(), false);
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

            Assert.AreEqual(testVariable.Type, AnyValueType.Integer);
            Assert.AreEqual(testVariable.AsInteger(), 42);
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

            Assert.AreEqual(testVariable.Type, AnyValueType.Float);
            Assert.AreEqual(testVariable.AsFloat(), -567.357);
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

            Assert.AreEqual(testVariable.Type, AnyValueType.String);
            Assert.AreEqual(testVariable.AsString(), "Hello world");
        }
    }
}
