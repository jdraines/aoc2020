using System;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day02Lib;

namespace AOC2020.DayPuzzles
{
    public class Day02Puzzle : IDayPuzzle
    {
        public Day02Puzzle()
        {
        }

        public string Day { get; } = "02";

        public void SolvePart1()
        {
            Solve(new P1PWPolicyChecker(), "1");
        }

        public void SolvePart2()
        {
            Solve(new P2PWPolicyChecker(), "1");
        }


        private void Solve(IPWPolicyChecker checker, string part)
        {
            int validCount = 0;
            string[] input = InputParser.ReadToStringArr(Day, "1");
            foreach (string line in input)
            {
                (string policy, string pwd) = Day02LineReader.SplitPolicyPwd(line);
                if (checker.PwdIsValid(policy, pwd))
                {
                    validCount++;
                }  
            }
            Console.WriteLine(validCount.ToString());
        }

        public void SolveTest() { }
    }
}
