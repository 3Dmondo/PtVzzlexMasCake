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
        internal static IDictionary<BigInteger, ICollection<ICollection<Operation>>> FindAllSolutions(int baseTwoExponent)
        {
            BigInteger maxValue = new BigInteger(1 << baseTwoExponent);
            var extracted = new Dictionary<BigInteger, ICollection<ICollection<Operation>>>();
            var costs = new Dictionary<BigInteger, int>();
            var queue = new PriorityQueue<Node, int>();
            var root = new Node(0, 1, new List<Operation>());
            queue.Enqueue(root, root.Cost);
            costs[root.Value] = 0;
            while (queue.TryDequeue(out var current, out _))
            {
                ICollection<ICollection<Operation>> solutions;
                if (!extracted.TryGetValue(current.Value, out solutions))
                {
                    solutions = new List<ICollection<Operation>>();
                    solutions.Add(current.Operations);
                    extracted.Add(current.Value, solutions);
                }
                else
                {
                    if (solutions.First().Count == current.Operations.Count) 
                    {
                        solutions.Add(current.Operations);
                    }
                }
                foreach (Node node in current.Star())
                {
                    if ((!costs.TryGetValue(node.Value, out var prevCost) ||
                        node.Cost <= prevCost) && node.Value <= maxValue)
                    {
                        costs[node.Value] = node.Cost;
                        queue.Enqueue(node, node.Cost);
                    }
                }

            }
            return extracted;
        }
    }
}

