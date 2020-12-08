using System;
using System.Collections.Generic;

namespace AOC2020.GBInterpreter
{
    public class GBExecutor
    {
        public static long BREAK { get; } = 9909909;
        public IInspector Inspector { get; set; } = new PassInspector();

        private Dictionary<string, ICommand> Command = new Dictionary<string, ICommand>
        {
            {"acc", new ACC() },
            {"jmp", new JMP() },
            {"nop", new NOP() },
        };

        private long Accumulator;
        private long linePointer;
        private string[] Code { get; set; }
        public GBCommand ExitCommand { get; set; }

        public GBExecutor(long accInit=0, long lineInit=1)
        {
            Accumulator = accInit;
            linePointer = lineInit;
            ExitCommand = new GBCommand("INITIALIZE", 0);
        }

        public void Restart(long accInit=0, long lineInit=1)
        {
            Accumulator = accInit;
            linePointer = lineInit;
            ExitCommand = new GBCommand("INITIALIZE", 0);
        }

        public long Load(string[] code)
        {
            Code = code;
            return 0;
        }

        public long Run(IInspector inspector=null, bool WriteOut=true)
        {
            if (inspector != null)
            {
                Inspector = inspector;
            }

            while (linePointer <= Code.Length)
            {
                GBCommand Cmd = ParseLine(Code[linePointer - 1]);
                long inspection = inspector.Inspect(ref Cmd, linePointer, Accumulator);
                if (BreakValue(inspection, Cmd))
                {
                    return Break(Cmd, WriteOut);
                }

                long execution = Command[Cmd.Name].Run(Cmd.Value, ref linePointer, ref Accumulator);
                if (BreakValue(execution, Cmd))
                {
                    return Break(Cmd, WriteOut);
                }
            }
            return Break(new GBCommand("COMPLETE", 0));
         
        }

        private GBCommand ParseLine(string line)
        {
            string[] name_val = line.Split(' ');
            string name = name_val[0];
            long val = Int64.Parse(name_val[1]);
            return new GBCommand(name, val);
        }

        private bool BreakValue(long value, GBCommand cmd)
        {
            if (value == BREAK)
            {
                return true;
            }

            return false;
        }

        private long Break(GBCommand cmd, bool WriteOut=true)
        {
            ExitCommand = cmd;
            if (WriteOut)
            {
                string msg = $"Breaking execution at line {linePointer}, command {cmd.ToString()}, with state Accumulator={Accumulator}";
                Console.WriteLine(msg);
            }
            return 0;
        }
    }
}