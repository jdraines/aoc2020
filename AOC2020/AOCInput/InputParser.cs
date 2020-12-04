using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AOC2020.AOCInput
{
    public static class InputParser
    {

        public static string[] ReadToStringArr(string day, string part)
        {
            string filename = $"Day{day}Input{part}.txt";
            string dirpath = Path.GetDirectoryName(GetThisFilePath());
            string inputPath = Path.Combine(dirpath, filename);
            string[] inputStrArr = File.ReadAllLines(inputPath);
            return inputStrArr;
        }

        public static int[] ReadToIntArr(string day, string part)
        {
            string[] strIn = ReadToStringArr(day, part);
            return ParseToInts(strIn);
        }

        private static int[] ParseToInts(string[] strArrIn)
        {
            int inputLength = strArrIn.Length;
            int[] intArr = new int[inputLength];

            for (int i = 0; i < inputLength; i++)
            {
                intArr[i] = Int32.Parse(strArrIn[i]);
            }
            return intArr;
        }

        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }
    }
}