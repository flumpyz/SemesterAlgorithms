using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SemesterAlgorithms
{
    public class Graph
    {
        private readonly List<Node> _nodes;

        public Graph()
        {
            _nodes = new List<Node>();
        }

        public void AddNode(int nodeNumber) // O(n)
        {
            if (ContainsNode(nodeNumber))
            {
                Console.WriteLine("Such a node already exists in this graph");
            }
            else
            {
                _nodes.Add(new Node(nodeNumber));
            }
        }

        public void AddEdge(int from, int to) // O(n + m)
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

        public void DeleteNode(int nodeNumber) // O(n^3)
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
            }
            else
            {
                Console.WriteLine("No such node exists in the given graph");
            }
        }

        public void DeleteEdge(int from, int to) // O(n^2) 
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

        public bool ContainsNode(int nodeNumber) // O(n)
        {
            return _nodes.Contains(new Node(nodeNumber));
        }

        public bool ContainsEdge(int from, int to) // O(n)
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
            var graph = new Graph();
            for (var i = 1; i <= nodesCount; i++)
            {
                graph.AddNode(i);
            }

            if (edgesCount > nodesCount * (nodesCount - 1))
            {
                throw new ArgumentException("This number of edges is impossible in this graph!");
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

        public List<(int, int)> FindMaxMeetings()
        {
            var meetings = new List<(int, int)>();
            var nodes = new List<int>();
            var meets = FindMutualEdges();
            var freq = CreateFrequencyNodes(meets)
                      .OrderBy(x => x.Value)
                      .ToList();
            
            while (freq.Count != 0)
            {
                var meet = meets.Find(x => x.Item1 == freq[0].Key || x.Item2 == freq[0].Key);
                if (!nodes.Contains(meet.Item1) && !nodes.Contains(meet.Item2) && meet.Item1 != meet.Item2)
                {
                    meetings.Add(meet);
                    nodes.Add(meet.Item1);
                    nodes.Add(meet.Item2);
                }
                freq.RemoveAt(0);
                meets.Remove(meet);
            }

            return meetings;
        }

        private List<(int, int)> FindMutualEdges()
        {
            var mutual = new HashSet<(int, int)>();
            var edges = new HashSet<Edge>(_nodes.SelectMany(node => node.edges));
            foreach (var edge in edges.Where(edge => edges.Contains(new Edge(edge.To, edge.From))))
            {
                mutual.Add((Math.Min(edge.From.Number, edge.To.Number), Math.Max(edge.From.Number, edge.To.Number)));
            }
            return mutual.ToList();
        }

        private Dictionary<int, int> CreateFrequencyNodes(List<(int, int)> meetings)
        {
            var dict = new Dictionary<int, int>();
            foreach (var meeting in meetings)
            {
                if (!dict.ContainsKey(meeting.Item1))
                {
                    dict.Add(meeting.Item1, 1);
                }
                else
                {
                    dict[meeting.Item1]++;
                }

                if (!dict.ContainsKey(meeting.Item2))
                {
                    dict.Add(meeting.Item2, 1);
                }
                else
                {
                    dict[meeting.Item2]++;
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
