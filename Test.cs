using Metric_Dimension;
using NUnit.Framework;

namespace MetricDimension
{
    [TestFixture]
    public class Test
    {
        private Graph TestGraph;

        [SetUp]
        protected void Setup()
        {
            TestGraph = new Graph();
            TestGraph.AppendNode(5);
            TestGraph.AppendEdge(1, 2);
            TestGraph.AppendEdge(1, 3);
            TestGraph.AppendEdge(2, 4);
            TestGraph.AppendEdge(3, 5);
            TestGraph.AppendEdge(2, 5);
            TestGraph.AppendEdge(3, 4);
        }

        [Test]
        public void NewGraph()
        {
            var graph = new Graph();
            Assert.NotNull(graph);
            Assert.AreEqual(graph.Dimension, 1);
        }

        [TestCase(1, int.MaxValue, 0, 1, 1, 2, 2)]
        [TestCase(2, int.MaxValue, 1, 0, 2, 1, 1)]
        [TestCase(0, 0, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue)]
        [TestCase(5, int.MaxValue, 2, 1, 1, 2, 0)]
        public void BFS(int start, params int[] dist)
        {
            var result = Algorithms.BFS(TestGraph, start);
            Assert.AreEqual(dist.Length, result.Length);
            for (int i = 0; i < dist.Length; i++)
                Assert.AreEqual(dist[i], result[i]);
        }

        [Test]
        public void ArgumentExp()
        {
            Assert.Throws<ArgumentException>(() => { TestGraph.AppendEdge(1, 9); });
        }

        [Test]
        public void IsResolv()
        {
            Assert.IsTrue(Algorithms.IsResolvingSet(TestGraph, new int[] { 0, 1, 1, 1, 1, 1 },
                Algorithms.FloydWarshallAlgorithm(TestGraph)));
        }
    }
}
