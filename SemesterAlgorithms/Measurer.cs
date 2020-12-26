using System;
using System.Diagnostics;

namespace SemesterAlgorithms
{
    public class Measurer
    {
        public static double MeasureAddNode(Graph gr, int nodesCount)
        {
            var stopwatch = new Stopwatch();

            gr.AddNode(nodesCount + 1);
            stopwatch.Start();
            gr.AddNode(nodesCount + 2);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double MeasureAddEdge(Graph gr, int nodesCount)
        {
            var stopwatch = new Stopwatch();
            var rnd = new Random();

            gr.AddEdge(nodesCount, nodesCount - 1);
            stopwatch.Start();
            gr.AddEdge(nodesCount, nodesCount - 2);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double MeasureDeleteNode(Graph gr, int nodesCount)
        {
            var stopwatch = new Stopwatch();

            gr.DeleteNode(nodesCount + 1);
            stopwatch.Start();
            gr.DeleteNode(nodesCount + 2);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double MeasureDeleteEdge(Graph gr, int nodesCount)
        {
            var stopwatch = new Stopwatch();
            
            gr.DeleteEdge(nodesCount, nodesCount - 1);
            stopwatch.Start();
            gr.DeleteEdge(nodesCount, nodesCount - 2);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double MeasureContainsNode(Graph gr, int nodesCount)
        {
            var stopwatch = new Stopwatch();

            gr.ContainsNode(nodesCount);
            stopwatch.Start();
            gr.ContainsNode(nodesCount);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public static double MeasureContainsEdge(Graph gr, int nodesCount)
        {
            var stopwatch = new Stopwatch();
            var rnd = new Random();

            var nodeNumberF = rnd.Next(1, nodesCount + 1);
            var nodeNumberS = rnd.Next(1, nodesCount + 1);
            gr.ContainsEdge(nodeNumberF, nodeNumberS);
            stopwatch.Start();
            gr.ContainsEdge(nodeNumberF, nodeNumberS);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}
