using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;
using InputUtil = AOC2020.DayLibs.Day16Lib.Day16InputUtil;
using TickProc = AOC2020.DayLibs.Day16Lib.D16TicketProcessor;

namespace AOC2020.DayPuzzles
{
    public class Day16Puzzle : IDayPuzzle
    {
        public string Day { get; } = "16";


        public void SolvePart1()
        {
            Tuple<string, HashSet<int>>[] rules = InputUtil.Rules();
            HashSet<int> masterSet = new HashSet<int>();
            foreach (var x in rules)
            {
                masterSet.UnionWith(x.Item2);
            }

            int[][] nearbyTickets = InputUtil.NearbyTickets();
            int invalidSum = 0;

            for (int i = 0; i < nearbyTickets.Length; i++)
            {
                foreach (int num in nearbyTickets[i])
                {
                    if (!masterSet.Contains(num))
                    {
                        invalidSum += num;
                    }
                }
            }

            Console.WriteLine($"Solution: {invalidSum}");
        }

        public void SolvePart2()
        {
            long solution = 1;
            Dictionary<string, List<int>> remainder = new Dictionary<string, List<int>>();
            Dictionary<string, int> fieldMap = new Dictionary<string, int>();
            HashSet<int> masterSet = new HashSet<int>();

            Tuple<string, HashSet<int>>[] rules = InputUtil.Rules();
            int[] myTicket = InputUtil.MyTicket();
            int[][] nearbyTickets = InputUtil.NearbyTickets();
            foreach (var x in rules) { masterSet.UnionWith(x.Item2); }

            List<int[]> validTickets = TickProc.ValidateTickets(nearbyTickets, masterSet);

            Dictionary<string, List<int>> possMap = TickProc.GetPossibleFieldMappings(validTickets, rules);

            do
            {
                remainder = TickProc.AssignPerfectFits(possMap, ref fieldMap);
                remainder = TickProc.AssignUniqueFits(remainder, ref fieldMap);
                possMap = remainder;
            }
            while (remainder.Count > 0);

            foreach (var item in fieldMap)
            {
                if (item.Key.StartsWith("departure"))
                {
                    solution *= (long)myTicket[item.Value];
                }
            }
            Console.WriteLine($"solution: {solution}");
        }

        public void SolveTest()
        {
            long solution = 1;
            Dictionary<string, List<int>> remainder = new Dictionary<string, List<int>>();
            Dictionary<string, int> fieldMap = new Dictionary<string, int>();
            HashSet<int> masterSet = new HashSet<int>();

            Tuple<string, HashSet<int>>[] rules = InputUtil.Rules();
            int[] myTicket = InputUtil.MyTicket();
            int[][] nearbyTickets = InputUtil.NearbyTickets();
            foreach (var x in rules) { masterSet.UnionWith(x.Item2); }

            List<int[]> validTickets = TickProc.ValidateTickets(nearbyTickets, masterSet);

            Dictionary<string, List<int>> possMap = TickProc.GetPossibleFieldMappings(validTickets, rules);

            int ITERATE_OUT = 10000;
            int i = 0;

            do
            {
                remainder = TickProc.AssignPerfectFits(possMap, ref fieldMap);
                remainder = TickProc.AssignUniqueFits(remainder, ref fieldMap);
                if (i > ITERATE_OUT)
                {
                    Console.WriteLine($"Max iterations reached. Remainder still contains {remainder.Count} item(s).");
                    break;
                }
                possMap = remainder;
            }
            while (remainder.Count > 0);


            Console.WriteLine($"field map count: {fieldMap.Count}");

            foreach (var item in fieldMap)
            {
                Console.WriteLine($"field: {item.Key}, idx: {item.Value}");

                if (item.Key.StartsWith("departure"))
                {
                    solution *= (long)myTicket[item.Value];
                }
            }
            Console.WriteLine($"solution: {solution}");
        }

    }
}
