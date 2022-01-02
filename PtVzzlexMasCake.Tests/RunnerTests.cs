using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PtVzzlexMasCake.Tests
{
    [TestClass]
    public class RunnerTests
    {
        [TestMethod]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(9)]
        [DataRow(11)]
        [DataRow(13)]
        [DataRow(15)]
        public void RunTest(int value)
        {
            Runner.FileName = value.ToString() + ".txt";
            Runner.Run(new string[] { value.ToString() });
            var expected = new StreamReader("Resources/" + value.ToString() + ".expected" + ".txt").ReadToEnd();
            var result = new StreamReader(Runner.FileName).ReadToEnd();
            Assert.AreEqual(expected, result);
        }
    }
}
