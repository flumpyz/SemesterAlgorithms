using System;
using System.Collections.Generic;

namespace SemesterAlgorithms
{
    public class Node
    {
        public readonly int Number;
        public readonly List<Edge> edges; 
        
        public Node(int number)
        {
            Number = number;
            edges = new List<Edge>();
        }

        public override bool Equals(Object obj)
        {
            var node = obj as Node;
            return Number == node.Number;
        }

        public override int GetHashCode()
        {
            return Number;
        }
    }
}
