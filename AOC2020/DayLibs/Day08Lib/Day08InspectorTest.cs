using System;
using System.Collections.Generic;
using AOC2020.GBInterpreter;

namespace AOC2020.DayLibs.Day08Lib
{
    public class D08P1InspectorTest : InspectorBase, IInspector
    {
        private long BREAK = GBExecutor.BREAK;

        private HashSet<long> linesRun = new HashSet<long>();

        public override long Inspect(ref GBCommand cmd, long linePointer, long accumulator)
        {
            Console.WriteLine($"Running line {linePointer}: {cmd.Name} {cmd.Value}, with state Accumulator={accumulator}\nLines Run: ");
            foreach (long num in linesRun)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine(" ");
            Console.ReadLine();

            if (linesRun.Contains(linePointer))
            {
                return BREAK;
            }
            else
            {
                linesRun.Add(linePointer);
            }
            return 0;
        }
    }
    public class D08P2InspectorTest : InspectorBase, IInspector
    {
        private long BREAK = GBExecutor.BREAK;
        private List<long> LinesChanged { get => UtilityListLong; set => UtilityListLong = value; }
        private bool mayEditCmd = true;

        private HashSet<long> linesRun = new HashSet<long>();

        public D08P2InspectorTest(List<long> linesChanged)
        {
            LinesChanged = linesChanged;
        }

        public override long Inspect(ref GBCommand cmd, long linePointer, long accumulator)
        {
            var switcher = new Dictionary<string, string>() { { "jmp", "nop" }, { "nop", "jmp" } };

            Console.WriteLine($"Running line {linePointer}: {cmd.Name} {cmd.Value}, with state Accumulator={accumulator}\nLines Run: ");
            foreach (long num in linesRun)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine(" ");
            Console.ReadLine();

            if (
                new HashSet<string>() { "jmp", "nop" }.Contains(cmd.Name) &&
                !LinesChanged.Contains(linePointer) &&
                mayEditCmd == true
                )
            {
                cmd.Name = switcher[cmd.Name];
                LinesChanged.Add(linePointer);
                mayEditCmd = false;
                Console.WriteLine($"    Command edited: {cmd.Name} {cmd.Value}");
            }

            if (linesRun.Contains(linePointer))
            {
                return BREAK;
            }
            else
            {
                linesRun.Add(linePointer);
            }
            return 0;
        }
    }
}
