using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;

namespace AOC2020.DayPuzzles
{
    public class Day15Puzzle : IDayPuzzle
    {
        public string Day { get; } = "15";

        private void StartStep(int num, ref int stepNum, ref List<int> history, ref Dictionary<int, List<int>> tally)
        {
            history.Add(num);

            // Handle tally for last number spoken
            if (stepNum > 0)
            {
                if (tally.ContainsKey(history[stepNum -1]))
                {
                    tally[history[stepNum - 1]].Add(stepNum);
                }
                else
                {
                    tally[history[stepNum - 1]] = new List<int>() { stepNum };
                }
            }

            stepNum++;
        }

        private void Step(int last, ref int stepNum, ref List<int> history, ref Dictionary<int, List<int>> tally)
        {
            int current = 0;

            if (tally.ContainsKey(last))
            {
                current = stepNum - tally[last].Last();
            }

            history.Add(current);

            // Handle tally for last number spoken
            if (stepNum > 0)
            {
                if (tally.ContainsKey(history[stepNum - 1]))
                {
                    tally[history[stepNum - 1]].Add(stepNum);
                }
                else
                {
                    tally[history[stepNum - 1]] = new List<int>() { stepNum };
                }
            }

            stepNum++;
        }

        private int Solution(int stop)
        {
            int[] start = InputParser.ReadTextToIntArr(Day);

            Dictionary<int, List<int>> tally = new Dictionary<int, List<int>>();
            List<int> history = new List<int>();

            int i;

            for (i = 0; i < start.Length; i += 0)
            {
                StartStep(start[i], ref i, ref history, ref tally);
            }

            while (i < stop)
            {
                Step(history[^1], ref i, ref history, ref tally);
            }

            return history[stop - 1];
        }

        public void SolvePart1()
        {
            int STOP = 2020;
            int solution = Solution(STOP);

            Console.WriteLine($"{STOP}th turn: {solution}");
        }

        public void SolvePart2()
        {
            int STOP = 2020;
            int solution = Solution(STOP);

            Console.WriteLine($"{STOP}th turn: {solution}");
        }

        public void SolveTest()
        { }
    }
}
