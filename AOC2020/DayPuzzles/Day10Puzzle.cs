using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day10Lib;

namespace AOC2020.DayPuzzles
{
    public class Day10Puzzle : IDayPuzzle
    {
        public string Day { get; } = "10";

        public void SolvePart1()
        {
            int[] adapterRatings = InputParser.ReadToIntArr(Day, "1");
            JoltAdapter[] adapters = new JoltAdapter[adapterRatings.Length];
            for(int i=0; i < adapterRatings.Length; i++)
            {
                adapters[i] = new JoltAdapter(adapterRatings[i]);
            }
            Device device = new Device(adapters.Select(a => a.Rating).Max() + 3);
            JoltAdapter[] chain = CreateSimpleChain(adapters);
            chain[^1].ConnectRecipient(device);
            int deviceDiff = device.inDiff;

            int oneDiffs = chain.Where(a => a.inDiff == 1).Count();
            int threeDiffs = chain.Where(a => a.inDiff == 3).Count();

            if (deviceDiff == 1) { oneDiffs++; }
            else if (deviceDiff == 3) { threeDiffs++; }
            Console.WriteLine($"Solution: {oneDiffs * threeDiffs}");
        }

        public void SolvePart2()
        { }

        public void SolveTest()
        { }

        private JoltAdapter[] CreateSimpleChain(JoltAdapter[] adapterArr)
        {
            JoltAdapter[] adapters = adapterArr.ToList().OrderBy(a=>a.Rating).ToArray();

            for (int i = 1; i < adapterArr.Length; i++)
            {
                if (i == 0)
                { }
                else if (CanConnect(adapters[i-1],adapters[i]))
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

        private bool CanConnect(IPowerRated a, IPowerRated b)
        {
            int diff = Math.Abs(b.Rating - a.Rating);
            return diff < 3 && diff >= 0;
        }
    }
}
