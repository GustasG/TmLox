using NUnit.Framework;

namespace TmLox.Interpreter.Tests
{
    public class VariableCreationTests
    {
        [Test]
        public void Test_Variable_Creation_WIthout_Explicityly_Assigning_Value_Should_Produce_Null_Valued_Value()
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
        public void Test_Variable_Creation_With_Null_Literal_Should_Produce_Null_Valued_Value()
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
        public void Test_Variable_Creation_With_False_Bool_Literal_Should_Produce_Boolean_Valued_Value_With_Value_As_False()
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
        public void Test_Variable_Creation_With_True_Bool_Literal_Should_Produce_Boolean_Valued_Value_With_Value_As_True()
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
        public void Test_Variable_Creation_With_Integer_Literal_Should_Produce_Integer_Valued_Value_With_Value_As_42()
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
        public void Test_Variable_Creation_With_Float_Literal_Should_Produce_Given_Float_Valued_Value()
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
        public void Test_Variable_Creation_With_String_Literal_Should_Produce_Given_String_Valued_Value()
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

        [Test]
        public void Test_Variable_Creation_With_Starting_Character_As_Underscode_And_Value_As_Integer_Literal_Should_Produce_Given_Integer_Value()
        {
            var code = @"
                var _tmp = -537;
            ";

            var script = new Script();
            script.RunString(code);

            var tmpVariable = script["_tmp"];

            Assert.True(tmpVariable.IsInteger());
            Assert.AreEqual(tmpVariable.AsInteger(), -537);
        }
    }
}