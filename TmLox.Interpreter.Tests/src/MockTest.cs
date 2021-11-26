using NUnit.Framework;

namespace TmLox.Interpreter.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestNothing()
        {
            Assert.AreEqual("Hello", "Hello");
        }
    }
}