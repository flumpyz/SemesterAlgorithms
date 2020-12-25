using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Reflection;

namespace SemesterAlgorithms
{
    public class Benchmark
    {
        public static double Run(List<(int, int)> nodesEdges)
        {
            TestAllGraphs(CreateTestGraphs(nodesEdges));

            return 0;
        }

        private static void TestAllGraphs(List<(Graph, int)> graphs)
        {
            foreach (var graph in graphs)
            {
                TestAllMethods(graph.Item1, graph.Item2);
            }
        }

        private static void TestAllMethods(Graph graph, int nodesCount)
        {
            WriteResultsInFile("AddNode", nodesCount, Measurer.MeasureAddNode(graph, nodesCount));
            WriteResultsInFile("AddEdge", nodesCount, Measurer.MeasureAddEdge(graph, nodesCount));
            WriteResultsInFile("DeleteNode", nodesCount, Measurer.MeasureDeleteNode(graph, nodesCount));
            WriteResultsInFile("DeleteEdge", nodesCount, Measurer.MeasureDeleteEdge(graph, nodesCount));
            WriteResultsInFile("ContainsNode", nodesCount, Measurer.MeasureContainsNode(graph, nodesCount));
            WriteResultsInFile("ContainsEdge", nodesCount, Measurer.MeasureContainsEdge(graph, nodesCount));
        }

        private static void WriteResultsInFile(string methodName, int itemCount, double time)
        {
            var path = $"C:/Users/{Environment.UserName}/Desktop/{methodName}.txt";
            var note = $"{itemCount} : {time} ms\r\n";
            using (var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                file.Seek(0, SeekOrigin.End);
                file.Write(Encoding.Default.GetBytes(note), 0, Encoding.Default.GetByteCount(note));
            }
        }

        private static List<(Graph, int)> CreateTestGraphs(List<(int, int)> nodesEdges)
        {
            var graphsAndCount = new List<(Graph, int)>();

            foreach (var item in nodesEdges)
            {
                graphsAndCount.Add((Graph.CreateRandomGraphWithEdges(item.Item1, item.Item2), item.Item1));
            }

            return graphsAndCount;
        }
    }
}
