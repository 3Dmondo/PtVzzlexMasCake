using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    internal static class Operator
    {
        public static BigInteger Execute(Operation operation, BigInteger value)
        {
            switch (operation)
            {
                case Operation.Halve:
                    return value / 2;
                case Operation.AddOne:
                    return value +1;
                case Operation.SubtractOne:
                    return value -1;
                default:
                    return value;
            }
        }
    }
}
