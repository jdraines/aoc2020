using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;


namespace AOC2020.DayLibs.Day16Lib
{
    public static class Day16InputUtil
    {

        public static Tuple<string, HashSet<int>>[] Rules(string day="16", string part = "1")
        {
            string rulesChunk = InputParser.ReadChunksToStringArr(day, part)[0];
            string[] rulesRaw = rulesChunk
                                    .Split('\n')
                                    .Where(x => !String.IsNullOrWhiteSpace(x))
                                    .ToArray();
            var rules = new Tuple<string, HashSet<int>>[rulesRaw.Length];
            string name;
            string rangeRaw;

            for (int i = 0; i < rulesRaw.Length; i++)
            {
                name = rulesRaw[i].Split(':')[0].Trim();
                rangeRaw = rulesRaw[i].Split(':')[1].Trim();
                rules[i] = new Tuple<string, HashSet<int>>(name, GetRange(rangeRaw));
            }
            return rules;
        }


        public static HashSet<int> GetRange(string rangeRaw)
        {
            int[] ParseRange(string r)
            {
                int[] ends = r.Split("-").Select(x => Int32.Parse(x.Trim())).ToArray();
                return Enumerable.Range(ends[0], ends[1] - ends[0] + 1).ToArray();
            }

            var set = new HashSet<int>();
            string[] ranges = rangeRaw.Split(" or ");
            foreach (var range in ranges)
            {
                set.UnionWith(ParseRange(range));
            }
            return set;
        }



        public static int[] MyTicket(string day = "16", string part = "1")
        {
            string myTickRaw = InputParser.ReadChunksToStringArr(day, part)[1].Split('\n')[1];
            return TickFromStr(myTickRaw);
        }


        public static int[][] NearbyTickets(string day = "16", string part = "1")
        {
            string[] tickStrs = InputParser.ReadChunksToStringArr(day, part)[2]
                                            .Split('\n')[1..]
                                            .Where(x => !String.IsNullOrWhiteSpace(x))
                                            .ToArray();

            return tickStrs
                    .Select(
                            x => x.Split(',')
                                    .Select(y => Int32.Parse(y))
                                    .ToArray()
                            ).ToArray();
        }


        public static int[] TickFromStr(string tickStr)
        {
            return tickStr.Split(",").Select(x => Int32.Parse(x)).ToArray();
        }


    }
}
