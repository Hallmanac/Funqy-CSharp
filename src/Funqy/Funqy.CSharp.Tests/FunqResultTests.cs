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


        [TestMethod]
        public void FunqResult_Of_T_Should_Return_False_For_HasValue_When_String_is_NullOrEmpty()
        {
            var funqResult = new FunqResult<string>(" ", true);
            var hasValue = funqResult.HasValue;

            Assert.IsTrue(!hasValue);
        }
    }
}
