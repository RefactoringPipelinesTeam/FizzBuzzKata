using ApprovalUtilities.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;

namespace FizzBuzzKata
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FizzBuzzTest()
        {
            // assert 1 =1 
            var result = _.FizzBuzz(1);
            Assert.AreEqual(result, "1");

        }
    }
}

static class _
{
    public static string FizzBuzz(int i)
    {
        return i.ToString();
    }
}