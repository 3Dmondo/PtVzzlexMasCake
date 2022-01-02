using PtVzzlexMasCake;
using System.Numerics;
Runner.Run(args);

public static class Runner
{
    public static string FileName { get; set; } = "output.txt"; 
    public static void Run(string[] args)
    {
        var value = BigInteger.Parse(args[0]);
        var solution = PuzzleSolver.Solve(value);
        using var sw = new StreamWriter(FileName);
        sw.WriteLine(value.ToString());
        foreach (var item in solution)
        {
            sw.WriteLine(item.OperationToString());
        }
    }
}