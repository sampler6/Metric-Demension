namespace Metric_Dimension
{
    public struct Edge
    {
        public int To { get; set; }

        public Edge (int To)
        {
            this.To = To;
        }
    }

    public class Graph
    {
        private readonly List<List<Edge>> graph;

        public Graph ()
        {
            graph = new List<List<Edge>>() { new List<Edge>() };
        }
        public int Dimension
        {
            get { return graph.Count(); }
        }

        public void AppendNode ()
        {
            graph.Add(new List<Edge>());
        }

        public void AppendNode (int count)
        {
            while (count-- != 0)
                graph.Add(new List<Edge>());
        }

        public void AppendEdge (int from, int to)
        {
            if (Dimension <= from || Dimension <= to)
                throw new ArgumentException();
            graph[from].Add(new Edge(to));
            graph[to].Add(new Edge(from));
        }

        public List<Edge> GetEdges (int Node)
        {
            if (Dimension <= Node)
                throw new ArgumentException();
            return graph[Node];
        }
    }
}
