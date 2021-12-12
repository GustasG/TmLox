namespace TmLox.Interpreter.Tests;

using System.Collections.Generic;

using NUnit.Framework;

public class ScriptEngineTests
{
    [Test]
    public void
        Test_Calling_Script_Engine_With_Non_Existant_Value_Should_Produce_Error_Stating_That_This_Variable_Does_Not_Exist()
    {
        var script = new Script();
        var ex = Assert.Throws<KeyNotFoundException>(delegate
        {
            var foo = script["foo"];
        });

        StringAssert.Contains("Variable with name foo does not exist", ex.Message);
    }
}