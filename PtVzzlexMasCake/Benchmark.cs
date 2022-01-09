using BenchmarkDotNet.Attributes;
using System.Numerics;

namespace PtVzzlexMasCake
{
    public class Benchmark
    {
        [Params(1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8, 1 << 9, 1 << 10)]
        public int Digits;

        private BigInteger Value;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var r = new Random(0);
            var s = new char[Digits];
            for (int i = 0; i < Digits; i++)
            {
                s[i] = r.Next(10).ToString().First();
            }
            Value = BigInteger.Parse(new string(s));
        }

        [Benchmark(Baseline = true)]
        public void PuzzleSolver()
        {
            _ = PtVzzlexMasCake.PuzzleSolver.Solve(Value);
        }
        [Benchmark]
        public void BinaryPuzzleSolver()
        {
            _ = PtVzzlexMasCake.BinaryPuzzleSolver.Solve(Value);
        }
    }
}
