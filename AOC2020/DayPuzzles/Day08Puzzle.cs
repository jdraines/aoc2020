using System;
using System.Collections.Generic;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day08Lib;
using AOC2020.GBInterpreter;

namespace AOC2020.DayPuzzles 
{
    public class Day08Puzzle : IDayPuzzle
    { 
        public string Day { get; } = "08";
        public void SolvePart1()
        {
            string[] code = InputParser.ReadToStringArr(Day, "1");
            GBExecutor exec = new GBExecutor();
            IInspector inspector = new D08P1Inspector();
            exec.Load(code);
            exec.Run(inspector);
        }
        public void SolvePart2()
        {
            string[] code = InputParser.ReadToStringArr(Day, "1");
            GBExecutor exec = new GBExecutor();
            D08P2Inspector inspector = new D08P2Inspector(new List<long>());
            exec.Load(code);
            int n = 10000;
            int i = 0;
            while (exec.ExitCommand.Name != "COMPLETE")
            {
                exec.Run(inspector, WriteOut: false);
                inspector = new D08P2Inspector(exec.Inspector.UtilityListLong);
                exec.Restart();
                if (i > n)
                    break;
                else
                    i++;
            }
        }
        public void SolveTest()
        {
            string[] code = InputParser.ReadToStringArr(Day, "Test");
            GBExecutor exec = new GBExecutor();
            D08P2InspectorTest inspector = new D08P2InspectorTest(new List<long>());
            exec.Load(code);
            int n = 10000;
            int i = 0;
            while (exec.ExitCommand.Name != "COMPLETE")
            {
                exec.Run(inspector);
                inspector = new D08P2InspectorTest(exec.Inspector.UtilityListLong);
                if (i > n)
                    break;
                else
                    i++;
            }
        }
    }
}
