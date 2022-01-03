namespace PtVzzlexMasCake
{
    internal enum Operation
    {
        Div,
        Add,
        Sub
    }

    internal static class OperationExtensions
    {
        public static string OperationToString(this Operation operation)
        {
            switch (operation)
            {
                case Operation.Add:
                    return nameof(Operation.Add);
                case Operation.Sub:
                    return nameof(Operation.Sub);
                case Operation.Div:
                    return nameof(Operation.Div);
                default:
                    return "";
            }
        }
    }
}
