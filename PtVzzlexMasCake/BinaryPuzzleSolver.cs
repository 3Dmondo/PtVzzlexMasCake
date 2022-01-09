using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;

namespace PtVzzlexMasCake
{
    internal static class BinaryPuzzleSolver
    {
        static Func<BigInteger, uint[]?> GetBits = CreateGetter<BigInteger, uint[]?>(typeof(BigInteger).GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance));
        static Func<BigInteger, int> GetSign = CreateGetter<BigInteger, int>(typeof(BigInteger).GetField("_sign", BindingFlags.NonPublic | BindingFlags.Instance));

        public static List<Operation> Solve(BigInteger value)
        {
            var solution = new List<Operation>();
            while (!value.IsOne)
            {
                if (value.IsEven)
                {
                    solution.Add(Operation.Div);
                    value = value >> 1;
                    continue;
                }

                if (value == 3)
                {
                    solution.Add(Operation.Sub);
                    value = value - 1;
                    continue;
                }
                var bits = GetBits(value);

                var firstBits = bits?[0] ?? (uint)GetSign(value);

                if ((firstBits & 3) == 3)
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
            return solution;
        }
        static Func<S, T> CreateGetter<S, T>(FieldInfo field)
        {
            string methodName = field.ReflectedType.FullName + ".get_" + field.Name;
            DynamicMethod getterrMethod = new DynamicMethod(methodName, typeof(T), new Type[1] { typeof(S) }, true);
            ILGenerator gen = getterrMethod.GetILGenerator();
            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Ldsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldfld, field);
            }
            gen.Emit(OpCodes.Ret);
            return (Func<S, T>)getterrMethod.CreateDelegate(typeof(Func<S, T>));
        }
    }
}
