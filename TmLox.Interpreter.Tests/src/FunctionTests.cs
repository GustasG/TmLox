using System.Collections.Generic;

using NUnit.Framework;

using TmLox.Interpreter.Errors;

namespace TmLox.Interpreter.Tests
{
    public class FunctionTests
    {
        private Script _script;


        [SetUp]
        public void SetUpScript()
        {
            var code = @"
                fun foo() {
                    return 42;
                }
            ";

            _script = new Script();
            _script.RunString(code);
        }

        [Test]
        public void Test_Calling_Lox_Function_From_Native_Code_Should_Produce_Valid_Returned_Integer_Value()
        {
            var foo = _script["foo"].AsFunction();
            var result = foo.Call(_script.Interpreter, new List<AnyValue>());

            Assert.True(result.IsInteger());
            Assert.AreEqual(42, result.AsInteger());
        }

        [Test]
        public void Test_Calling_Lox_Function_Inside_Interpreter_Should_Produce_Valid_Returned_Integer_Value()
        {
            var fooResultCode = @"
                var foo_result = foo();
            ";

            _script.RunString(fooResultCode);
            var fooResult = _script["foo_result"];

            Assert.True(fooResult.IsInteger());
            Assert.AreEqual(42, fooResult.AsInteger());
        }

        [Test]
        public void Test_Calling_Lox_Function_With_Incorrect_Number_Of_Arguments_Should_Produce_Error_Abount_Expected_Argument_Count()
        {
            var incorrectFooResult = @"
                var tmp = foo(1, 2, 3);
            ";

            var ex = Assert.Throws<ValueError>(delegate
            {
                _script.RunString(incorrectFooResult);
            });

            StringAssert.Contains("Function foo expects 0 arguments, while 3 arguments were provided", ex.Message);
        }
    }
}
