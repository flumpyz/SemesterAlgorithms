using System;
using System.Collections.Generic;
using System.Linq;

namespace SemesterAlgorithms
{
    public class Graph
    {
        private readonly List<Node> Nodes;
        
        public Graph()
        {
            Nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
        
        public void AddEdge(Node from, Node to)
        {
            if (!this.Nodes.Contains(from) || !this.Nodes.Contains(to))
            {
                throw new ArgumentException();
            }
            var edge = new Edge(from, to);
            from.edges.Add(edge);
            to.edges.Add(edge);
        }

        public void DeleteNode(Node node)
        {
            if (!(Nodes.Contains(node)))
            {
                Console.WriteLine("No such node exists in the given graph");
                return;
            }
            
            foreach (var edge in node.edges)
            {
                if (node.Equals(edge.From))
                {
                    edge.To.edges.Remove(edge);
                }
                else
                {
                    edge.From.edges.Remove(edge);
                }
            }

            Nodes.Remove(node);

            //Console.WriteLine("No such node exists in the given graph");
        }

        public void DeleteEdge(Edge edge)
        {
            edge.From.edges.Remove(edge);
            edge.To.edges.Remove(edge);
        }

        public bool ContainsNode(Node node)
        {
            return Nodes.Contains(node);
        }
    }
}
