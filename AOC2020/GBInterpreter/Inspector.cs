using System;
using System.Collections.Generic;

namespace AOC2020.GBInterpreter
{

    public interface IInspector
    {
        List<long> UtilityListLong { get; set; }
        List<string> UtilityListString { get; set; }
        long Inspect(ref GBCommand cmd, long linePointer, long accumulator);
    }

    public class PassInspector : IInspector
    {
        public List<long> UtilityListLong { get; set; } = new List<long>();
        public List<string> UtilityListString { get; set; } = new List<string>();

        public long Inspect(ref GBCommand cmd, long linePointer, long accumulator)
        {
            return 0;
        }

    }

    public class InspectorBase : IInspector
    {
        public List<long> UtilityListLong { get; set; } = new List<long>();
        public List<string> UtilityListString { get; set; } = new List<string>();

        public virtual long Inspect(ref GBCommand cmd, long linePointer, long accumulator)
        {
            return 0;
        }

    }
}