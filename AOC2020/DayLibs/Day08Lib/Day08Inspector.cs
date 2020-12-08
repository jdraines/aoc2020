using System;
using System.Collections.Generic;
using AOC2020.GBInterpreter;

namespace AOC2020.DayLibs.Day08Lib
{ 
    public class D08P1Inspector : InspectorBase, IInspector
    {
        private long BREAK = GBExecutor.BREAK;

        private HashSet<long> linesRun = new HashSet<long>();

        public override long Inspect(ref GBCommand cmd, long linePointer, long accumulator)
        {
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

    public class D08P2Inspector : InspectorBase, IInspector
    {
        private long BREAK = GBExecutor.BREAK;
        private List<long> LinesChanged { get => UtilityListLong; set => UtilityListLong = value; }
        private bool mayEditCmd = true;

        private HashSet<long> linesRun = new HashSet<long>();

        public D08P2Inspector(List<long> linesChanged)
        {
            LinesChanged = linesChanged;
        }

        public override long Inspect(ref GBCommand cmd, long linePointer, long accumulator)
        {
            var switcher = new Dictionary<string, string>() { { "jmp", "nop" }, { "nop", "jmp" } };
            
            if (
                new HashSet<string>() { "jmp", "nop" }.Contains(cmd.Name) &&
                !LinesChanged.Contains(linePointer) &&
                mayEditCmd == true
                )
            {
                cmd.Name = switcher[cmd.Name];
                LinesChanged.Add(linePointer);
                mayEditCmd = false;
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
