using System;
using System.Collections.Generic;

namespace AOC2020.GBInterpreter
{
    /// <summary>
    /// The GBExecutor (Game Boy Executor) class runs the code from the Day08
    /// input.
    ///
    /// Its constructor allows one to instatiate the object with code, and
    /// initialization values for the Accumulator and linePointer. In addition
    /// to internally setting these properties, initialization also sets initial
    /// values for the ExitCommand and History properties.
    ///
    /// The Reset method allows one to reset the instance to an initialized state.
    ///
    /// The Run method executes the code and allows for an injected inspector
    /// to perform inspection operations and return a break code if desired.
    /// 
    /// Inspectors inspect each line of code. While the inspector cannot alter
    /// the linePointer or accumulator values, it can alter the single command
    /// that it inspects, enabling some debugging.
    /// </summary>
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
        public List<GBCommand> History;

        public GBExecutor(string[] code, long accInit=0, long lineInit=1)
        {
            Reset(code, accInit, lineInit);
        }
        ///<summary>
        /// The Reset method allows one to reset the instance to an initialized state.
        /// </summary>
        public void Reset(string[] code=null, long accInit=0, long lineInit=1)
        {
            if (code is null)
            {
                Code = code;
            }
            Accumulator = accInit;
            linePointer = lineInit;
            ExitCommand = new GBCommand("INITIALIZE", 0);
            History = new List<GBCommand>();
        }

        /// <summary>
        /// The Run method executes the code and allows for an injected inspector
        /// to perform inspection operations and return a break code if desired.
        /// 
        /// Inspectors inspect each line of code. While the inspector cannot alter
        /// the linePointer or accumulator values, it can alter the single command
        /// that it inspects, enabling some debugging.
        /// </summary>
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
                History.Add(Cmd);
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