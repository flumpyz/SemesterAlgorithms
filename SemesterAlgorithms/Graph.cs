using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SemesterAlgorithms
{
    public class Graph
    {
        private readonly List<Node> _nodes;
        private readonly HashSet<Node> _infNodes;

        public Graph()
        {
            _nodes = new List<Node>();
            _infNodes = new HashSet<Node>();
        }

        public void AddNode(int nodeNumber) // O(1)
        {
            if (ContainsNode(nodeNumber))
            {
                Console.WriteLine("Such a node already exists in this graph");
            }
            else
            {
                _nodes.Add(new Node(nodeNumber));
                _infNodes.Add(new Node(nodeNumber));
            }
        }

        public void AddEdge(int from, int to) 
        {
            if (ContainsNode(from) && ContainsNode(to))
            {
                if (!ContainsEdge(from, to))
                {
                    var nodeFrom = _nodes.Find(n => n.Number == from); 
                    var nodeTo = _nodes.Find(n => n.Number == to); 
                    nodeFrom.edges.Add(new Edge(nodeFrom, nodeTo));
                    nodeTo.edges.Add(new Edge(nodeFrom, nodeTo));
                }
                else
                {
                    Console.WriteLine("Such edge exists in the given graph");
                }
            }
            else
            {
                Console.WriteLine("No such nodes exists in the given graph");
            }
        }

        public void DeleteNode(int nodeNumber) 
        {
            if (ContainsNode(nodeNumber)) 
            {
                var node = _nodes.Find(n => n.Number == nodeNumber); 
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
                _nodes.Remove(node);
                _infNodes.Remove(node);
            }
            else
            {
                Console.WriteLine("No such node exists in the given graph");
            }
        }

        public void DeleteEdge(int from, int to) 
        {
            if (ContainsNode(from) && ContainsNode(to)) 
            {
                if (ContainsEdge(from, to)) 
                {
                    var nodeFrom = _nodes.Find(n => n.Number == from); 
                    var nodeTo = _nodes.Find(n => n.Number == to); 
                    nodeFrom.edges.Remove(new Edge(nodeFrom, nodeTo)); 
                    nodeTo.edges.Remove(new Edge(nodeFrom, nodeTo)); 
                }
                else
                {
                    Console.WriteLine("No such edge exists in the given graph");
                }
            }
            else
            {
                Console.WriteLine("No such nodes exists in the given graph");
            }
        }

        public bool ContainsNode(int nodeNumber) 
        {
            return _infNodes.Contains(new Node(nodeNumber));
        }

        public bool ContainsEdge(int from, int to) 
        {
            if (ContainsNode(from) && ContainsNode(to)) 
            {
                var nodeFrom = _nodes.Find(n => n.Number == from); 
                var nodeTo = _nodes.Find(n => n.Number == to); 
                var edge = new Edge(nodeFrom, nodeTo);

                return nodeFrom.edges.Contains(edge) && nodeTo.edges.Contains(edge); 
            }

            return false;
        }


        public static Graph CreateRandomGraphWithEdges(int nodesCount, int edgesCount)
        {
            if (edgesCount > nodesCount * (nodesCount - 1))
            {
                throw new ArgumentException("This number of edges is impossible in this graph!");
            }
            var graph = new Graph();
            for (var i = 1; i <= nodesCount; i++)
            {
                graph.AddNode(i);
            }
            var hashSet = new HashSet<(int, int)>();
            if (edgesCount != 0)
            {
                var rnd = new Random();
                while (hashSet.Count <= edgesCount)
                {
                    var first = rnd.Next(1, nodesCount + 1);
                    var second = rnd.Next(1, nodesCount + 1);
                    if (first != second)
                    {
                        hashSet.Add((first, second));
                    }
                }
            }
            foreach (var item in hashSet)
            {
                graph.AddEdge(item.Item1, item.Item2);
            }
            return graph;
        }

        public void MakeGraph(List<(int, int)> edges)
        {
            foreach (var edge in edges)
            {
                if (!ContainsNode(edge.Item1))
                {
                    AddNode(edge.Item1);
                }
                if (!ContainsNode(edge.Item2))
                {
                    AddNode(edge.Item2);
                }
                AddEdge(edge.Item1, edge.Item2);
            }
        }

        public List<(int, int)> FindMaxMeetings() // O(n*m)
        {
            var meetings = new HashSet<(int, int)>();
            var nodes = new HashSet<int>();
            var meets = FindMutualEdges(); // O(m)
            var freq = CreateFrequencyNodes(meets) // O(m)
                      .OrderBy(x => x.Value) // O(n * logn)
                      .ToList(); // O(n) => O(n * logn + m)
            
            
            while (freq.Count != 0) // O(n) => O(n*m)
            {
                var meet = meets.Find(x => x.Item1 == freq[0].Key || x.Item2 == freq[0].Key); // O(m)
                if (!nodes.Contains(meet.Item1) && !nodes.Contains(meet.Item2) && meet.Item1 != meet.Item2) // O(1)
                {
                    meetings.Add(meet); // O(1)
                    nodes.Add(meet.Item1); // O(1)
                    nodes.Add(meet.Item2); // O(1)
                }
                freq.RemoveAt(0); // O(n)
                meets.Remove(meet); // O(m)
            }

            return meetings.ToList(); // O(m)
        }

        private List<(int, int)> FindMutualEdges() // Поиск "взаимных" рёбер - O(m)
        {
            var mutual = new HashSet<(int, int)>();
            var edges = new HashSet<Edge>(_nodes.SelectMany(node => node.edges)); // O(m)
            foreach (var edge in edges.Where(edge => edges.Contains(new Edge(edge.To, edge.From)))) // O(m)
            {
                mutual.Add((Math.Min(edge.From.Number, edge.To.Number), Math.Max(edge.From.Number, edge.To.Number))); // O(1)
            }
            return mutual.ToList(); // O(n)
        }

        private Dictionary<int, int> CreateFrequencyNodes(List<(int, int)> meetings) // Создание частотного словаря по вершинам - O(m)
        {
            var dict = new Dictionary<int, int>();
            foreach (var meeting in meetings) // O(m)
            {
                if (!dict.ContainsKey(meeting.Item1)) // O(1)
                {
                    dict.Add(meeting.Item1, 1); // O(1)
                }
                else
                {
                    dict[meeting.Item1]++; // O(1)
                }

                if (!dict.ContainsKey(meeting.Item2))
                {
                    dict.Add(meeting.Item2, 1); // O(1)
                }
                else
                {
                    dict[meeting.Item2]++; // O(1)
                }
            }
            return dict;
        }

        public List<(int, int)> FindMaxMeetingsByBruteForce()
        {
            var allPaths = BruteForce(FindMutualEdges());
            return FindBestSequence(allPaths); 
        }

        private List<List<(int, int)>> BruteForce(List<(int, int)> edges)
        {
            var all = new List<List<(int, int)>>();
            var count = Math.Pow(2, edges.Count);
            for (var i = 1; i < count; i++)
            {
                var str = Convert.ToString(i, 2).PadLeft(edges.Count, '0');
                var sequence = new List<(int, int)>();
                var nodes = new HashSet<int>();
                for (var j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1' && !nodes.Contains(edges[j].Item1) && !nodes.Contains(edges[j].Item2))
                    {
                        sequence.Add(edges[j]);
                        nodes.Add(edges[j].Item1);
                        nodes.Add(edges[j].Item2);
                    }
                }
                all.Add(sequence);
            }
            return all;
        }

        private List<(int, int)> FindBestSequence(List<List<(int, int)>> allOptions)
        {
            var max = 0;
            var bestOption = new List<(int, int)>();
            foreach (var option in allOptions)
            {
                if (option.Count > max)
                {
                    max = option.Count;
                    bestOption = option;
                }
            }
            return bestOption;
        }
    }
}
