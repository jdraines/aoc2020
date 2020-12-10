﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AOC2020.AOCInput
{
    public static class InputParser
    {

        public static string[] ReadToStringArr(string day, string part, bool dropBlanks=false)
        {
            string filename = $"Day{day}Input{part}.txt";
            string dirpath = Path.GetDirectoryName(GetThisFilePath());
            string inputPath = Path.Combine(dirpath, filename);
            string[] inputStrArr = File.ReadAllLines(inputPath);
            if (dropBlanks)
            {
                List<string> noBlanks = new List<string>();
                foreach (string line in inputStrArr)
                {
                    string noBreak = line.Replace("\n", "");
                    if (!String.IsNullOrWhiteSpace(noBreak))
                    {
                        noBlanks.Add(noBreak);
                    }
                }
                inputStrArr = noBlanks.ToArray();
            }
            return inputStrArr;
        }

        public static string[] ReadChunksToStringArr(string day, string part)
        {
            string filename = $"Day{day}Input{part}.txt";
            string dirpath = Path.GetDirectoryName(GetThisFilePath());
            string inputPath = Path.Combine(dirpath, filename);
            string[] inputStrArr = File.ReadAllLines(inputPath);

            List<string> chunks = new List<string>();

            string chunk = "";

            for (int i=0; i < inputStrArr.Length; i++)
            {
                string head = inputStrArr[i];
                if (!String.IsNullOrWhiteSpace(head))
                {
                    chunk += (head + " ");
                }
                else
                {
                    chunks.Add(chunk);
                    chunk = "";
                }

                if (i == inputStrArr.Length - 1)
                {
                    chunks.Add(chunk);
                }
            }
            return chunks.ToArray();
        }

        public static int[] ReadToIntArr(string day, string part)
        {
            string[] strIn = ReadToStringArr(day, part);
            return ParseToInts(strIn);
        }

        public static long[] ReadToLongArr(string day, string part)
        {
            string[] strIn = ReadToStringArr(day, part);
            return ParseToLongs(strIn);
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

        private static long[] ParseToLongs(string[] strArrIn)
        {
            int inputLength = strArrIn.Length;
            long[] intArr = new long[inputLength];

            for (int i = 0; i < inputLength; i++)
            {
                intArr[i] = Int64.Parse(strArrIn[i]);
            }
            return intArr;
        }


        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }
    }
}