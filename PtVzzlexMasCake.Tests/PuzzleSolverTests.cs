using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    [TestClass]
    public class PuzzleSolverTests
    {
        const int baseTwoExponent = 7;

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
                    initialValue = Operator.Execute(operation, initialValue);
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

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        [DataRow(100000)]
        public void SolveBigNumber(int digits)
        {
            var r = new Random(0);
            var s = new char[digits];
            for (int i = 0; i < digits; i++)
            {
                s[i] = r.Next(10).ToString().First();
            }
            var value = BigInteger.Parse(new string(s));
            var solution = PuzzleSolver.Solve(value);
            var initialValue = value;
            foreach (var operation in solution)
            {
                initialValue = Operator.Execute(operation, initialValue);
            }
            Assert.AreEqual(1, initialValue);
        }

        [TestMethod]
        public void FindAllSolutions()
        {
            var result = DijkstraSolver.FindAllSolutions(baseTwoExponent);
            var maxSolutions = result.Values.Max(x => x.Count);
            var numberWithMaxSolutions = result.Where(x => x.Value.Count == maxSolutions).ToList();
            using var sw = new StreamWriter("../../../multipleSolutions.txt");
            foreach (var item in numberWithMaxSolutions)
            {
                sw.WriteLine(item.Key);
                foreach (var item2 in item.Value)
                {
                    sw.WriteLine(string.Join(" ", item2.Reverse()));
                }
                sw.WriteLine();
            }
            var drawingGraph = new Graph();
            var interestingNodes = result; // result.Where(x => x.Value.Count == 1);
            foreach (var item in interestingNodes)
            {
                var node = new Microsoft.Msagl.Drawing.Node(item.Key.ToString());
                node.LabelText = item.Key.ToString();
                node.Label.FontColor = item.Key.IsEven ? Color.DarkGreen : Color.Red;
                drawingGraph.AddNode(node);
            }
            foreach (var item in interestingNodes)
            {
                if (item.Value.Any(x => x.Count > 0))
                {
                    foreach (var item1 in item.Value.Select(x => x.Last()).Distinct())
                    {
                        drawingGraph.AddEdge(
                            item.Key.ToString(),
                            Operator.Execute(item1, item.Key).ToString()).
                            LabelText = item1.ToString();
                    }
                }
            }
            drawingGraph.CreateGeometryGraph();


            // Now the drawing graph elements point to the corresponding geometry elements, 
            // however the node boundary curves are not set.
            // Setting the node boundaries
            foreach (var n in drawingGraph.Nodes)
            {
                // Ideally we should look at the drawing node attributes, and figure out, the required node size
                // I am not sure how to find out the size of a string rendered in SVG. Here, we just blindly assign to each node a rectangle with width 60 and height 40, and round its corners.
                n.GeometryNode.BoundaryCurve = CurveFactory.CreateRectangleWithRoundedCorners(40, 40, 5, 5, new Point(0, 0)); // .CreateCircle(20, new Point(0, 0)); //

            }

            AssignLabelsDimensions(drawingGraph);

            LayoutHelpers.CalculateLayout(drawingGraph.GeometryGraph, new SugiyamaLayoutSettings(), null);
            PrintSvgAsString(drawingGraph);
        }
        static void AssignLabelsDimensions(Graph drawingGraph)
        {
            // In general, the label dimensions should depend on the viewer
            foreach (var na in drawingGraph.Nodes)
            {
                na.Label.Width = 40;
                na.Label.Height = 40;
            }

            // init geometry labels as well
            foreach (var de in drawingGraph.Edges)
            {
                // again setting the dimensions, that should depend on Drawing.Label and the viewer, blindly
                de.Label.GeometryLabel.Width = 40;
                de.Label.GeometryLabel.Height = 40;
       
            }
        }

        static void PrintSvgAsString(Graph drawingGraph)
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            var svgWriter = new SvgGraphWriter(writer.BaseStream, drawingGraph);
            svgWriter.Write();
            // get the string from MemoryStream
            ms.Position = 0;
            var sr = new StreamReader(ms);
            var myStr = sr.ReadToEnd();
            using var sw = new StreamWriter("../../../../graph.svg");
            sw.Write(myStr);
        }
    }

}