using PtVzzlexMasCake;
using System.Numerics;
using BenchmarkDotNet.Running;

if (args.Contains("-benchmark"))
{
    BenchmarkRunner.Run<Benchmark>();
}
else
{
    Runner.Run(args);
}

public static class Runner
{
    public static string FileName { get; set; } = "output.txt";
    const int digits = 309;
    public static void Run(string[] args)
    {
        BigInteger value = 0;
        if (args.Length == 1)
        {
            value = BigInteger.Parse(args[0]);
        }
        else
        {
            var r = new Random();
            var s = new char[digits];
            for (int i = 0; i < digits; i++)
            {
                s[i] = r.Next(10).ToString().First();
            }
            value = BigInteger.Parse(new string(s));
        }
        var solution = BinaryPuzzleSolver.Solve(value);
        using var sw = new StreamWriter(FileName);
        sw.WriteLine(value.ToString());
        foreach (var item in solution)
        {
            sw.WriteLine(item.OperationToString());
        }
    }
}