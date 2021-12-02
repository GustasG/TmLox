using NUnit.Framework;

namespace TmLox.Interpreter.Tests
{
    public class IfTests
    {
        [Test]
        public void Test_Simple_If_Condition_It_Should_Modify_Given_Variable()
        {
            var code = @"
                var starting = 0;

                if (true) {
                    starting = 5;
                }
            ";

            var script = new Script();
            script.RunString(code);

            var startingValue = script["starting"];

            Assert.True(startingValue.IsInteger());
            Assert.AreEqual(5, startingValue.AsInteger());
        }

        [Test]
        public void Test_Else_If_Execution_From_After_If_Condition_Should_Execute_Given_Else_If_Block()
        {
            var code = @"
                var starting = 0;

                if (false) {
                    starting = 5;
                } elif (true) {
                    starting = 10;
                }
            ";

            var script = new Script();
            script.RunString(code);

            var startingValue = script["starting"];

            Assert.True(startingValue.IsInteger());
            Assert.AreEqual(10, startingValue.AsInteger());
        }

        [Test]
        public void Test_Else_Execution_After_If_Condition_Should_Execute_Given_Else_Block()
        {
            var code = @"
                var starting = 0;

                if (false) {
                    starting = 5;
                } else {
                    starting = 10;
                }
            ";

            var script = new Script();
            script.RunString(code);

            var startingValue = script["starting"];

            Assert.True(startingValue.IsInteger());
            Assert.AreEqual(10, startingValue.AsInteger());
        }

        [Test]
        public void Test_Else_Execution_After_If_Condition_And_Multiple_Else_If_Conditions_Should_Execute_Given_Else_Block()
        {
            var code = @"
                var starting = 0;

                if (false) {
                    starting = 5;
                } elif (false) {
                    starting = 10;
                } elif (false) {
                    starting = 15;
                } else {
                    starting = 20;
                }
            ";

            var script = new Script();
            script.RunString(code);

            var startingValue = script["starting"];

            Assert.True(startingValue.IsInteger());
            Assert.AreEqual(20, startingValue.AsInteger());
        }
    }
}