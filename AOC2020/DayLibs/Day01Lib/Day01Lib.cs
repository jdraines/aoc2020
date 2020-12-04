using System;

namespace AOC2020.DayLibs.Day01Lib
{
    public static class Day01EntryBalancer
    {

        public static ValueTuple<int, int> PairsSumToVal(int[] allEntries, int val)
        {
            (int, int) notFound = (0, 0);

            for (int i = 0; i < allEntries.Length; i++)
            {
                for (int j = i; j < allEntries.Length; j++)
                {
                    int a = allEntries[i];
                    int b = allEntries[j];

                    if (SumEqualsVal(a, b, val))
                    {
                        return (a, b);
                    }
                        
                }
            }
            return notFound;
        }

        public static ValueTuple<int, int, int> TriosSumToVal(int[] allEntries, int val)
        {
            (int, int, int) notFound = (0, 0, 0);

            for (int i = 0; i < allEntries.Length; i++)
            {
                for (int j = 0; j < allEntries.Length; j++)
                {
                    for (int k = 0; k < allEntries.Length; k++)
                    {
                        int a = allEntries[i];
                        int b = allEntries[j];
                        int c = allEntries[k];

                        if (SumEqualsVal(a, b, c, val))
                        {
                            return (a, b, c);
                        }
                    }
                }
            }
            return notFound;
        }

        private static bool SumEqualsVal(int a, int b, int val)
        {
            return a + b == val;
        }

        private static bool SumEqualsVal(int a, int b, int c, int val)
        {
            return a + b + c == val;
        }
    }
}