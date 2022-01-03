using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    internal class Node : IEquatable<Node?>
    {
        public int Cost { get; }
        public BigInteger Value { get; }
        public List<Operation> Operations { get; }

        public Node(int cost, BigInteger value, List<Operation> operations)
        {
            Cost = cost;
            Value = value;
            Operations = operations;
        }

        internal IEnumerable<Node> Star()
        {
            var divideByTwo = new List<Operation>(Operations);
            divideByTwo.Add(Operation.Div);
            yield return new Node(Cost + 1, Value * 2, divideByTwo);

            if (Value > 1)
            {
                var addOne = new List<Operation>(Operations);
                addOne.Add(Operation.Add);
                yield return new Node(Cost + 1, Value - 1, addOne);

                var subtractOne = new List<Operation>(Operations);
                subtractOne.Add(Operation.Sub);
                yield return new Node(Cost + 1, Value + 1, subtractOne);
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Node);
        }

        public bool Equals(Node? other)
        {
            return other != null &&
                   Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}