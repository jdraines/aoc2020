using System;
using System.Linq;
using AOC2020.AOCInput;

namespace AOC2020.DayPuzzles
{
    public class Day13Puzzle : IDayPuzzle
    {
        public string Day { get; } = "13";

        private string[] getInput(string part="1")
        {
            return InputParser.ReadToStringArr(Day, part);
        }

        private long departureTime(string part="1")
        {
            return Int64.Parse(getInput(part)[0]);
        }

        private bool[] GenDepartureRow(long time, int[] bLines)
        {
            bool[] depRow = new bool[bLines.Length];

            for (int i=0; i < bLines.Length; i++)
            {
                if (bLines[i] == 0)
                {
                    depRow[i] = false;
                }
                else
                {
                    depRow[i] = ((time % bLines[i]) == 0);
                }
            }
            return depRow;
        }

        private long FindNextDeparture(int bLine, long startTime=0)
        {
            long i = startTime;

            while((i % bLine) != 0)
            {
                i++;
            }
            return i;
        }

        private bool IsDeparting(long time, int bLine)
        {
            return (time % bLine) == 0;
        }

        private int[] busLines(string part = "1")
        {
            string raw = getInput(part)[1];
            string[] raws = raw.Split(',');
            int[] busLines = new int[raws.Length];
            string bLine;
            for (int i=0; i<raws.Length; i++)
            {
                bLine = raws[i];
                if (bLine == "x")
                {
                    bLine = "0";
                }
                busLines[i] = Int32.Parse(bLine);
            }
            return busLines;
        }

        public void SolvePart1()
        {
            long depTime = departureTime();
            int[] bLines = busLines();
            bool[] row;

            long i = depTime;
            long waitTime = 0;
            int departingLine = 0;
            while (departingLine == 0)
            {
                row = GenDepartureRow(i, bLines);
                for (int j = 0; j < bLines.Length; j++)
                {
                    if (row[j])
                    {
                        departingLine = bLines[j];
                        waitTime = i - depTime;
                        break;
                    }
                }
                i++;
            }
            Console.WriteLine($"Earliest departing bus line: {departingLine}, minutes waited: {waitTime}, solution: {departingLine * waitTime}");
        }

        public void SolvePart2()
        {
            int[] bLines = busLines();
            int maxBLine = bLines.Max();
            int idxMaxBLine = Array.IndexOf(bLines, maxBLine);
            int firstLine = maxBLine;
            long depTimeMax = 0;
            long i = 100000000000000;
            long stepTime = 0;
            bool solved = false;


            while (!solved)
            {
                if (depTimeMax == 0)
                {
                    depTimeMax = FindNextDeparture(firstLine, i);
                }
                stepTime = depTimeMax - idxMaxBLine;
                solved = true;
                for (int j = 0; j < bLines.Length; j++)
                {
                    if (bLines[j] == 0)
                    {
                        stepTime++;
                        continue;
                    }
                    else if (IsDeparting(stepTime, bLines[j]))
                    {
                        stepTime++;
                    }
                    else
                    {
                        solved = false;
                        break;
                    }
                }
                if (!solved)
                {
                    depTimeMax += maxBLine;
                }
            }

            Console.WriteLine($"Solution: {depTimeMax - idxMaxBLine}");
        }

        public void SolveTest()
        {
            int[] bLines = busLines("Test");
            int maxBLine = bLines.Max();
            int idxMaxBLine = Array.IndexOf(bLines, maxBLine);
            int firstLine = maxBLine;
            long depTimeMax = 0;
            long i = 0;
            long stepTime = 0;
            bool solved = false;


            while (!solved)
            {
                if (depTimeMax == 0)
                {
                    depTimeMax = FindNextDeparture(firstLine, i);
                }
                stepTime = depTimeMax - idxMaxBLine;
                solved = true;
                for (int j = 0; j < bLines.Length; j++)
                {
                    if (bLines[j] == 0)
                    {
                        stepTime++;
                        continue;
                    }
                    else if (IsDeparting(stepTime, bLines[j]))
                    {
                        stepTime++;
                    }
                    else
                    {
                        solved = false;
                        break;
                    }
                }
                if (!solved)
                {
                    depTimeMax += maxBLine;
                }
            }

            Console.WriteLine($"Solution: {depTimeMax - idxMaxBLine}");
        }
    }
}
