using System;

namespace SemesterAlgorithms
{
    public class Edge // from first to second
    {
        public readonly Node From;
        public readonly Node To;
        
        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }

        public override bool Equals(Object obj)
        {
            var edge = obj as Edge;
            return Equals(From, edge.From) && Equals(To, edge.To);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((From != null ? From.GetHashCode() : 0) * 397) ^ (To != null ? To.GetHashCode() : 0);
            }
        }
    }
}
