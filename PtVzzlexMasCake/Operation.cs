namespace PtVzzlexMasCake
{
    internal enum Operation
    {
        Halve,
        AddOne,
        SubtractOne
    }

    internal static class OperationExtensions
    {
        public static string OperationToString(this Operation operation)
        {
            switch (operation)
            {
                case Operation.AddOne:
                    return nameof(Operation.AddOne);
                case Operation.SubtractOne:
                    return nameof(Operation.SubtractOne);
                case Operation.Halve:
                    return nameof(Operation.Halve);
                default:
                    return "";
            }
        }
    }
}
