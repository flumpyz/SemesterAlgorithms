using System;
using System.Collections.Generic;
using System.Linq;

namespace SemesterAlgorithms
{
    static class Program
    {
        static void Main()
        {
            // Console.WriteLine(Benchmark.Run((new List<(int, int)>()
            //                                     {
            //                                         (10, 1),
            //                                         (100, 1),
            //                                         (500, 1),
            //                                         (1000, 1),
            //                                         (5000, 1),
            //                                         (10000, 1)
            //                                     })));
            var gr = new Graph();
            gr.MakeGraph(new List<(int, int)>()
            {
                (1, 2),
                (2, 3),
                (3, 2),
                (2, 6),
                (1, 4),
                (4, 1),
                (6, 4),
                (4, 5),
                (5, 6),
                (6, 5)
            });
            var meetings = gr.FindMaxMeetings();
            var meetingsBy = gr.FindMaxMeetingsByBruteForce();

            foreach (var meeting in meetings)
            {
                Console.WriteLine(meeting.ToString());
            }

            Console.WriteLine();

            foreach (var meetingBy in meetingsBy)
            {
                Console.WriteLine(meetingBy.ToString());
            }
        }
    }
}
