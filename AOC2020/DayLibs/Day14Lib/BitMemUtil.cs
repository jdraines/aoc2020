using System;
using System.Linq;
using System.Collections.Generic;


namespace AOC2020.DayLibs.Day14Lib
{
    public static class BitMemUtil
    {


        public static string GetMask(string line)
        {
            return line.Split(" = ")[1];
        }

        public static string ToBit36(string intString)
        {
            return Convert.ToString(Int32.Parse(intString), 2).PadLeft(36, '0');
        }

        public static string ToBit36(long longInt)
        {
            return Convert.ToString(longInt, 2).PadLeft(36, '0');
        }

        public static string ToBit36(int int32)
        {
            return Convert.ToString(int32, 2).PadLeft(36, '0');
        }



        public static Tuple<long, string> ParseMemSet(string line)
        {
            string[] parts = line.Split(" = ");
            long keyLong = Int64.Parse(parts[0].Split('[')[1].Split(']')[0]);
            string bitStr = ToBit36(parts[1]);
            return new Tuple<long, string>(keyLong, bitStr);
        }

        public static Tuple<List<string>, long> ParseMemSet(string line, string mask)
        {
            string[] parts = line.Split(" = ");
            long keyLong = Int64.Parse(parts[0].Split('[')[1].Split(']')[0]);
            long val = Int64.Parse(parts[1]);
            string masked = ApplyMaskP2(ToBit36(keyLong), mask);
            return new Tuple<List<string>, long>(GenFloatPermutations(masked), val);
        }

        public static string ApplyMaskP1(string int36, string mask)
        {
            char[] masked = new char[36];

            for (int i = 0; i < 36; i++)
            {
                if (mask[i] == 'X')
                {
                    masked[i] = int36[i];
                }

                else if (mask[i] == '1')
                {
                    masked[i] = '1';
                }
                else if (mask[i] == '0')
                {
                    masked[i] = '0';
                }
            }
            return new string(masked);
        }

        public static string ApplyMaskP2(string int36, string mask)
        {
            char[] masked = new char[36];
            for (int i = 0; i < 36; i++)
            {
                if (mask[i] == 'X')
                {
                    masked[i] = 'X';
                }

                else if (mask[i] == '1')
                {
                    masked[i] = '1';
                }
                else if (mask[i] == '0')
                {
                    masked[i] = int36[i];
                }
            }
            return new string(masked);
        }


        public static List<string> GenFloatPermutations(string bitStr)
        {
            char[] newStr;
            List<string> permutations = new List<string>();
            var indices = new List<int>();

            for (int i = 0; i < bitStr.Length; i++)
            {
                if (bitStr[i] == 'X')
                {
                    indices.Add(i);
                }
            }

            Console.WriteLine($"indices: {String.Join(", ", indices.Select(xi => xi.ToString()))}\nbitStr: {bitStr}");

            if (indices.Count == 0)
            {
                return new List<string>() { bitStr };
            }

            List<List<bool>> boolPerms = GenPermutations(bitStr, indices.Count, new List<List<bool>>());

            for (int i = 0; i < boolPerms.Count; i++)
            {
                newStr = bitStr.ToArray();
                for (int j = 0; j < indices.Count; j++)
                {
                    if (boolPerms[i][j])
                    {
                        newStr[indices[j]] = '1';
                    }
                    else
                    {
                        newStr[indices[j]] = '0';
                    }
                }
                permutations.Add(new string(newStr));
            }
            return permutations;
        }

        public static List<List<bool>> GenPermutations(string bitStr, int length, List<List<bool>> memo)
        {
            var _master = new List<List<bool>>();
            var _memo = new List<List<bool>>();
            memo = memo.Select(x => x).ToList();

            if (length == 1)
            {
                foreach (bool val in new bool[] { true, false })
                {
                    memo.Add(new List<bool> { val });
                }

                return memo;
            }
            else
            {
                foreach (bool val in new bool[] { true, false })
                {
                    _memo = GenPermutations(bitStr, length - 1, memo);

                    foreach (var perm in _memo)
                    {
                        perm.Add(val);
                    }
                    _master.AddRange(_memo);
                }
                return _master;
            }
        }


    }
}
