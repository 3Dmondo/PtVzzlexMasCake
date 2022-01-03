using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    internal static class Operator
    {
        public static BigInteger Execute(Operation operation, BigInteger value)
        {
            switch (operation)
            {
                case Operation.Div:
                    return value / 2;
                case Operation.Add:
                    return value +1;
                case Operation.Sub:
                    return value -1;
                default:
                    return value;
            }
        }
    }
}
