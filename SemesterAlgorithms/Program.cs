using System;

namespace SemesterAlgorithms
{
    static class Program
    {
        static void Main()
        {
            var graph = new Graph();
            
            var node1 = new Node(1);
            var node2 = new Node(2);
            var node3 = new Node(3);
            
            var edge1 = new Edge(node3, node1);
            var edge2 = new Edge(node3, node2);
            
            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            
            graph.AddEdge(node1, node2);
            graph.AddEdge(node3, node1);
            
            graph.DeleteEdge(edge1);
            graph.DeleteEdge(edge2);
            
            graph.DeleteNode(node1);
            graph.DeleteNode(node1);
            
            Console.WriteLine(graph.ContainsNode(node2));
            
            graph.DeleteNode(node2);
            
            Console.WriteLine(graph.ContainsNode(node1));
        }
    }
}
