using System;
using System.Collections.Generic;
using System.Linq;

namespace SemesterAlgorithms
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine(Benchmark.Run((new List<(int, int)>()
                                                {
                                                    (10, 1),
                                                    (100, 1),
                                                    (500, 1),
                                                    (1000, 1),
                                                    (5000, 1),
                                                    (10000, 1)
                                                })));
            // var meetings = Graph.CreateRandomGraphWithEdges(10, 45).FindMaxMeetings();
            // foreach (var meeting in meetings)
            // {
            //     Console.WriteLine(meeting.ToString());
            // }
        }
    }
}
