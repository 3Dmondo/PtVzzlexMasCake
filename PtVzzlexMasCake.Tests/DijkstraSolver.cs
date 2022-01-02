using System.Numerics;

namespace PtVzzlexMasCake.Tests
{
    public static class DijkstraSolver
    {
        /// <summary>
        /// compute the shortest path from Pow(2, <paramref name="baseTwoExponent"/>) to 1
        /// using the allowed operations [+1,-1,/2]
        /// </summary>
        /// <param name="baseTwoExponent">exponent of the base two power representing the maximum value</param>
        internal static IEnumerable<Node> SolveFull(int baseTwoExponent)
        {
            BigInteger maxValue = new BigInteger(1 << baseTwoExponent);
            var extracted = new HashSet<Node>();
            var costs = new Dictionary<BigInteger, int>();
            var queue = new PriorityQueue<Node, int>();
            var root = new Node(0, 1, new List<Operation>());
            queue.Enqueue(root, root.Cost);
            costs[root.Value] = 0;
            while (queue.TryDequeue(out var current, out _))
            {
                if (extracted.Add(current))
                {
                    foreach (Node node in current.Star())
                    {
                        if ((!costs.TryGetValue(node.Value, out var prevCost) ||
                            node.Cost < prevCost) && node.Value <= maxValue)
                        {
                            costs[node.Value] = node.Cost;
                            queue.Enqueue(node, node.Cost);
                        }
                    }
                }
            }
            return extracted;
        }
    }
}
