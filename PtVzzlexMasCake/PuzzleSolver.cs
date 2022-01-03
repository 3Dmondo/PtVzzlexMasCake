using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PtVzzlexMasCake
{
    internal static class PuzzleSolver
    {
        public static List<Operation> Solve(BigInteger value)
        {
            var solution = new List<Operation>();
            while (value != 1)
            {
                if (value.IsEven)
                {
                    solution.Add(Operation.Div);
                    value = value / 2;
                }
                else
                {
                    if (value == 3)
                    {
                        solution.Add(Operation.Sub);
                        value = value - 1;
                    }
                    else if (((value + 1) / 2).IsEven)
                    {
                        solution.Add(Operation.Add);
                        value = value + 1;
                    }
                    else
                    {
                        solution.Add(Operation.Sub);
                        value = value - 1;
                    }
                }
            }
            return solution;
        }
    }
}
