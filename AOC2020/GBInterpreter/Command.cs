using System;

namespace AOC2020.GBInterpreter
{
    public struct GBCommand
    {
        public string Name;
        public long Value;
        public GBCommand(string name, long value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"<{Name.ToUpper()}: {Value}>";
        }
    }
    public interface ICommand
    {
        long Run(long value, ref long linePointer, ref long accumulator);

    }

    public class JMP : ICommand
    {

        public long Run(long value, ref long linePointer, ref long accumulator)
        {
            linePointer += value;
            return 0;
        }
    }

    public class ACC : ICommand
    {
        public long Run(long value, ref long linePointer, ref long accumulator)
        {
            accumulator += value;
            linePointer++;
            return 0;
        }
    }
    public class NOP : ICommand
    {
        public long Run(long value, ref long linePointer, ref long accumulator)
        {
            linePointer++;
            return 0;
        }
    }

}
