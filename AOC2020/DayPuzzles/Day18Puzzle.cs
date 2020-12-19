using System;
using System.Collections.Generic;
using AOC2020.AOCInput;

namespace AOC2020.DayPuzzles
{
    public class Day18Puzzle : IDayPuzzle
    {
        public string Day { get; } = "18";

        public Day18Puzzle()
        {
            Operators["add"] = new Func<long, long, long>(Addition);
            Operators["multiply"] = new Func<long, long, long>(Multiply);
        }

        public static long Addition(long a, long b)
        {
            return a + b;
        }

        public long Multiply(long a, long b)
        {
            return a * b;
        }

        public Dictionary<string, Func<long, long, long>> Operators = new Dictionary<string, Func<long, long, long>>();


        public (long, int) DoMath(string line)
        {
            char current;
            long currentNum = 0;
            long lastNum = 0;
            string pendingAction = "None";
            int idx = 0;
            int offset = 0;

            for (int i=0; i < line.Length; i++)
            {
                current = line[i];

                switch (current)
                {
                    case ' ':
                        continue;

                    case '+':
                        pendingAction = "add";
                        break;

                    case '*':
                        pendingAction = "multiply";
                        break;

                    case '(':
                        idx = i + 1;
                        (currentNum, offset) = DoMath(line[idx..]);
                        i = idx + offset;
                        break;

                    case ')':
                        return (lastNum, i);

                    default:
                        currentNum = Int64.Parse(current.ToString());
                        break;
                }

                if (pendingAction != "None" && currentNum != -1)
                {
                    lastNum = Operators[pendingAction](lastNum, currentNum);
                    pendingAction = "None";
                    currentNum = -1;
                }
                else if (currentNum != -1)
                {
                    lastNum = currentNum;
                    currentNum = -1;
                }
            }
            return (lastNum, -1);
        }

        public (long, int) DoAdvancedMath(string line)
        {
            char current;
            long currentNum = -1;
            long lastNum = 0;
            string pendingAction = "None";
            int idx = 0;
            int offset = 0;

            for (int i = 0; i < line.Length; i++)
            {
                current = line[i];

                switch (current)
                {
                    case ' ':
                        continue;

                    case '+':
                        pendingAction = "add";
                        break;

                    case '*':
                        pendingAction = "multiply";
                        idx = i + 1;
                        (currentNum, offset) = DoAdvancedMath(line[idx..]); // needs a reason to return
                        i = idx + offset;
                        if (i != line.Length - 1)
                        {
                            i--; // handles the case where fn is returned due to parentheses, rather than end of line
                                 // In that case, it needs to re-evaluate the parenthesis and perform another return
                                 // so the index should be set to encounter the close parenthesis again.
                        }
                        break;

                    case '(':
                        idx = i + 1;
                        (currentNum, offset) = DoAdvancedMath(line[idx..]);
                        i = idx + offset;
                        break;

                    case ')':
                        return (lastNum, i);

                    default:
                        currentNum = Int64.Parse(current.ToString());
                        break;
                }

                if (pendingAction != "None" && currentNum != -1)
                {
                    lastNum = Operators[pendingAction](lastNum, currentNum);
                    pendingAction = "None";
                    currentNum = -1;
                }
                else if (currentNum != -1)
                {
                    lastNum = currentNum;
                    currentNum = -1;
                }
            }
            return (lastNum, line.Length - 1);
        }

        public void SolvePart1()
        {
            string[] lines = InputParser.ReadToStringArr(Day);
            long[] nums = new long[lines.Length];
            long solution = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                (nums[i], _) = DoMath(lines[i]);
                solution += nums[i];
            }
            Console.WriteLine($"Solution: {solution}");
        }

        public void SolvePart2()
        {
            string[] lines = InputParser.ReadToStringArr(Day);
            long[] nums = new long[lines.Length];
            long solution = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                (nums[i], _) = DoAdvancedMath(lines[i]);
                solution += nums[i];
            }
            Console.WriteLine($"Solution: {solution}");
        }

        public void SolveTest()
        {
            string[] lines = InputParser.ReadToStringArr(Day, "Test");
            long[] nums = new long[lines.Length];
            long solution = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                (nums[i], _) = DoAdvancedMath(lines[i]);
                solution += nums[i];
            }
            Console.WriteLine($"Solution: {solution}");
        }
    }
}
