using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    [TestClass]
    public class PuzzleSolverTests
    {
        const int baseTwoExponent = 20;

        [TestMethod]
        public void PuzzleSolverTest()
        {
            using var sw = new StreamWriter("../../../multipleSolutions.txt");
            using var sw2 = new StreamWriter("../../../costs.txt");

            var result = DijkstraSolver.SolveFull(baseTwoExponent);

            foreach (var item in result)
            {
                var solution = PuzzleSolver.Solve(item.Value);

                //check that the operation sequence leads to the correct value
                var initialValue = item.Value;
                foreach (var operation in solution)
                {
                    initialValue = Operator.Execute(operation,initialValue);
                }
                Assert.AreEqual(1, initialValue);

                item.Operations.Reverse();
                //check that the cost is equal to the minimum one
                Assert.AreEqual(item.Operations.Count, solution.Count);
                //cannot compare the solutions because there may be multiple
                //solutions for the same integer
                //all occurrencies of multiple equivalent solutions can be written in the file
                //multipleSolutions.txt setting the following condition to true
                if (false)
                {
                    var zip = item.Operations.Zip(solution).ToArray();
                    if (zip.Any(x => x.First != x.Second))
                    {
                        sw.WriteLine(item.Value);
                        foreach (var x in zip)
                        {
                            sw.WriteLine($"{x.First} {x.Second}");
                        }
                        sw.WriteLine();
                    }
                }

                if (true)
                {
                    sw2.WriteLine($"{item.Value} {item.Cost}");
                }
            }
        }
    }
}