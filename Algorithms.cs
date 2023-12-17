namespace Metric_Dimension
{
    public static class Algorithms
    {
        public static int[] BFS (Graph graph, int startNode)
        {
            var distance = new int[ graph.Dimension ];
            for (var i = 0; i < graph.Dimension; i++)
                distance[ i ] = int.MaxValue;

            var q = new Queue<int>();
            q.Enqueue(startNode);
            int timer = 0;
            while (q.Count > 0)
            {
                int sz = q.Count;
                for (var i = 0; i < sz; i++)
                {
                    var node = q.Dequeue();
                    if (distance[ node ] < timer)
                        continue;
                    distance[ node ] = timer;
                    var edges = graph.GetEdges(node);
                    foreach (var edge in edges)
                        q.Enqueue(edge.To);
                }
                timer++;
            }
            return distance;
        }

        public static int[,] FloydWarshallAlgorithm (Graph graph)
        {
            int[,] distance = new int[ graph.Dimension, graph.Dimension ];
            for (var i = 0; i < graph.Dimension; i++)
                for (var j = 0; j < graph.Dimension; j++)
                    distance[ i, j ] = int.MaxValue;
            for (var i = 0; i < graph.Dimension; i++)
            {
                distance[ i, i ] = 0;
                var edges = graph.GetEdges(i);
                foreach (var edge in edges)
                {
                    distance[ i, edge.To ] = 1;
                }
            }

            for (var i = 0; i < graph.Dimension; i++)
                for (var j = 0; j < graph.Dimension; j++)
                    for (var k = 0; k < graph.Dimension; k++)
                    {
                        if (distance[ j, i ] == int.MaxValue || distance[ i, k ] == int.MaxValue)
                            continue;
                        distance[ j, k ] = Math.Min(distance[ j, k ], distance[ j, i ] + distance[ i, k ]);
                    }

            return distance;
        }

        public static bool IsResolvingSet (Graph graph, int[] set, int[,] distances)
        {
            var isResolvingSet = true;
            for (var i = 1; i < graph.Dimension; i++)
            {
                for (var j = 1; j < graph.Dimension; j++)
                {
                    if (i == j)
                        continue;
                    var isEqual = true;
                    for (var vertex = 0; vertex < set.Length; vertex++)
                    {
                        if (set[ vertex ] == 0)
                            continue;
                        if (distances[ i, vertex ] != distances[ j, vertex ])
                        {
                            isEqual = false;
                        }
                    }
                    if (isEqual)
                    {
                        return false;
                    }
                }
            }

            return isResolvingSet;
        }

        private static void GenerateBitMask (int length, int currentPosition, int numberOfNotUsedNodes, List<int[]> Bitmasks, int[] currentBitmask)
        {
            if (currentPosition == length)
            {
                if (numberOfNotUsedNodes != 0)
                    return;
                var temp = new int[ currentBitmask.Length ];
                Array.Copy(currentBitmask, temp, currentBitmask.Length);
                Bitmasks.Add(temp);
                return;
            }
            if (numberOfNotUsedNodes > 0)
            {
                currentBitmask[ currentPosition ] = 1;
                GenerateBitMask(length, currentPosition + 1, numberOfNotUsedNodes - 1, Bitmasks, currentBitmask);
            }
            currentBitmask[ currentPosition ] = 0;
            GenerateBitMask(length, currentPosition + 1, numberOfNotUsedNodes, Bitmasks, currentBitmask);
        }

        public static int[] BruteForceSolution (Graph graph, int[,] distances)
        {
            var resolvingSet = Array.Empty<int>();
            var IsSolutionFound = false;
            for (var i = 1; i < graph.Dimension; i++)
            {
                if (IsSolutionFound)
                    break;
                var Bitmasks = new List<int[]>();
                var currentBitmask = new int[ graph.Dimension ];
                currentBitmask[ 0 ] = 0;
                GenerateBitMask(graph.Dimension, 1, i, Bitmasks, currentBitmask);

                for (var j = 0; j < Bitmasks.Count; j++)
                {
                    if (IsResolvingSet(graph, Bitmasks[ j ], distances))
                    {
                        resolvingSet = Bitmasks[ j ];
                        IsSolutionFound = true;
                        break;
                    }
                }
            }

            return resolvingSet;
        }

        public static int[] BinarySearchSolution (Graph graph, int[,] distances)
        {
            var resolvingSet = Array.Empty<int>();
            int left = 0, right = graph.Dimension;
            List<int[]> Bitmasks;
            int[] currentBitmask;
            while (right - left != 1)
            {
                var middle = (right + left) / 2;
                var IsSolutionFound = false;
                Bitmasks = new List<int[]>();
                currentBitmask = new int[ graph.Dimension ];
                currentBitmask[ 0 ] = 0;
                GenerateBitMask(graph.Dimension, 1, middle, Bitmasks, currentBitmask);

                for (var j = 0; j < Bitmasks.Count; j++)
                {
                    if (IsResolvingSet(graph, Bitmasks[ j ], distances))
                    {
                        resolvingSet = Bitmasks[ j ];
                        IsSolutionFound = true;
                        break;
                    }
                }
                if (IsSolutionFound)
                    right = middle;
                else
                    left = middle;
            }
            Bitmasks = new List<int[]>();
            currentBitmask = new int[ graph.Dimension ];
            currentBitmask[ 0 ] = 0;
            GenerateBitMask(graph.Dimension, 1, right, Bitmasks, currentBitmask);

            for (var j = 0; j < Bitmasks.Count; j++)
            {
                if (IsResolvingSet(graph, Bitmasks[ j ], distances))
                {
                    resolvingSet = Bitmasks[ j ];
                    break;
                }
            }
            return resolvingSet;
        }
    }
}
