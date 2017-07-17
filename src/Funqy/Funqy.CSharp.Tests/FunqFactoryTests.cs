using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Funqy.CSharp.Tests
{
    [TestClass]
    public class FunqFactoryTests
    {
        [TestMethod]
        public void The_Then_Method_Should_Chain_Together_Strings()
        {
            var someResult = "First Line".GetFunqy()
                                         .Then(val =>
                                         {
                                             var sb = new StringBuilder();
                                             sb.AppendLine(val);
                                             sb.AppendLine("Second Line");
                                             var strValue = sb.ToString();
                                             return FunqFactory.Ok<string>(strValue, "The second line was added successfully");
                                         })
                                         .Then(val =>
                                         {
                                             var sb = new StringBuilder();
                                             sb.Append(val);
                                             sb.AppendLine("Third Line");
                                             var strValue = sb.ToString();
                                             return FunqFactory.Ok<string>(strValue, "The third line was added successfully");
                                         });
            var expectedSb = new StringBuilder();
            expectedSb.AppendLine("First Line");
            expectedSb.AppendLine("Second Line");
            expectedSb.AppendLine("Third Line");
            var expected = expectedSb.ToString();

            Assert.AreEqual(expected, someResult.Value);
        }


        [TestMethod]
        public void The_Catch_Method_Should_Handle_The_Error()
        {
            string someLogger = null;
            var someResult = "First Line".GetFunqy()
                                         .Then(val =>
                                         {
                                             var sb = new StringBuilder();
                                             sb.AppendLine(val);
                                             sb.AppendLine("Second Line");
                                             var strValue = sb.ToString();
                                             return FunqFactory.Ok<string>(strValue, "The second line was added successfully");
                                         })
                                         .Then(val =>
                                         {
                                             var sb = new StringBuilder();
                                             return FunqFactory.Fail<string>("I forgot what number comes after two!!", val);
                                         })
                                         .Catch(resultSoFar =>
                                         {
                                             if (resultSoFar.IsSuccessful)
                                             {
                                                 return FunqFactory.Ok<string>(resultSoFar.Value, "Everything Was a success");
                                             }
                                             // Write out the current error message to a log
                                             someLogger = $"I just logged a message. Here is the error:\n{resultSoFar.Message}";
                                             return FunqFactory.Fail<string>("You suck!", resultSoFar.Value);
                                         });

            var expectedSb = new StringBuilder();
            expectedSb.AppendLine("First Line");
            expectedSb.AppendLine("Second Line");
            var expected = expectedSb.ToString();

            Assert.AreEqual(expected, someResult.Value);
            Assert.AreEqual("I just logged a message. Here is the error:\nI forgot what number comes after two!!", someLogger);
        }
    }
}
