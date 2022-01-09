using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    [TestClass]
    public class BinaryPuzzleSolverTests
    {
        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        public void SolveBigNumber(int digits)
        {
            var r = new Random(0);
            var s = new char[digits];
            for (int i = 0; i < digits; i++)
            {
                s[i] = r.Next(10).ToString().First();
            }
            var value = BigInteger.Parse(new string(s));

            var expectedSolution = PuzzleSolver.Solve(value);
            var solution = BinaryPuzzleSolver.Solve(value);
            CollectionAssert.AreEqual(expectedSolution, solution);

            var initialValue = value;
            foreach (var operation in solution)
            {
                initialValue = Operator.Execute(operation, initialValue);
            }
            Assert.AreEqual(1, initialValue);
        }
    }
}
