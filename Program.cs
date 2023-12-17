using Metric_Dimension;

namespace MetricDimension
{
    public class MetricDimensionCalculation
    {
        public static void Input(Graph graph)
        {
            IEnumerable<string>? input = File.ReadLines("input.txt");
            bool isFirstLine = true;
            foreach (var line in input)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    graph.AppendNode(Convert.ToInt32(line));
                    continue;
                }
                int from, to;
                var numbers = line.Split(' ');
                from = Convert.ToInt32(numbers[0]);
                to = Convert.ToInt32(numbers[1]);
                graph.AppendEdge(from, to);
            }
        }

        public static void Output(Graph graph, int[] resolvingSetBitmask, int[,] distances)
        {
            List<int> resolvingSet = new List<int>();
            for (int i = 0; i < resolvingSetBitmask.Length; i++)
            {
                if (resolvingSetBitmask[i] == 1)
                {
                    resolvingSet.Add(i);
                }
            }
            string output = "Метрическая размерность графа: " + resolvingSet.Count.ToString() + "\nПример разрешающего множества: ";
            for (int i = 0; i < resolvingSet.Count; i++)
                output += resolvingSet[i].ToString() + " ";
            output += "\nМетрические представления вершин:\n";
            for (int i = 1; i < graph.Dimension; i++)
            {
                output += i + " = {";
                for (int j = 0; j < resolvingSet.Count; j++)
                {
                    output += distances[i, resolvingSet[j]];
                }
                output += "}\n";
            }
            File.WriteAllText("output.txt", output);
        }

        public static int Main()
        {
            Graph graph = new Graph();
            Input(graph);
            var distances = Algorithms.FloydWarshallAlgorithm(graph);
            //var resolvingSet = Algorithms.BruteForceSolution(graph, distances);
            var resolvingSet = Algorithms.BinarySearchSolution(graph, distances);
            Output(graph, resolvingSet, distances);


            return 0;
        }
    }
}