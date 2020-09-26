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
            var result = FizzBuzz.Convert(1);
            Assert.AreEqual(result, "1");

        }
    }
}