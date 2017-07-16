using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Funqy.CSharp.Tests
{
    [TestClass]
    public class FunqResultTests
    {
        [TestMethod]
        public void FunqResult_Should_Fail_If_Not_Successful_And_Message_Is_Null_Or_WhiteSpace()
        {
            try
            {
                var funqResult = FunqFactory.Fail(" ");
                Assert.Fail("InvalidOperationException was not thrown as expected.");
            }
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("No error message provided for a non-successful value", e.Message, "The expected messages didn't match up. Please correct the \"expected\" value");
            }
        }
    }
}
