using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020.DayLibs.Day10Lib
{
    public static class Utilities
    {

        public static JoltAdapter[] SortAdapters(JoltAdapter[] adapters)
        {
            return adapters.ToList().OrderBy(a => a.Rating).ToArray();
        }

        public static int GetDeviceRating(JoltAdapter[] adapters)
        {
            return adapters.Select(a => a.Rating).Max() + 3;
        }

        public static JoltAdapter[] CreateSimpleChain(JoltAdapter[] adapterArr)
        {
            JoltAdapter[] adapters = SortAdapters(adapterArr);

            for (int i = 1; i < adapterArr.Length; i++)
            {
                if (i == 0)
                { }
                else if (CanConnect(adapters[i - 1], adapters[i]))
                {
                    adapters[i].ConnectSource(adapters[i - 1]);
                }
                else
                {
                    throw new Exception($"Cannot connect thes two consecutive adapters rated {adapters[i - 1].Rating} & {adapters[i].Rating}");
                }
            }

            return adapters;
        }
        public static bool CanConnect(IPowerRated a, IPowerRated b)
        {
            int diff = Math.Abs(b.Rating - a.Rating);
            return diff < 3 && diff >= 0;
        }
    }
}
