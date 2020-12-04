using System;
using ParsedPolicy = System.ValueTuple<System.ValueTuple<int, int>, char>;

namespace AOC2020.DayLibs.Day02Lib
{
    public class P1PWPolicyChecker : IPWPolicyChecker
    {
        public P1PWPolicyChecker()
        {
        }

        public bool PwdIsValid(string policy, string pwd)
        {
            ParsedPolicy pPolicy  = ParsePolicy(policy);
            int count = CountOccurence(pPolicy.Item2, pwd);
            return count >= pPolicy.Item1.Item1 && count <= pPolicy.Item1.Item2;
        }

        private ParsedPolicy ParsePolicy(string policy)
        {
            string[] range_letter = policy.Split(' ');
            string[] range_str = range_letter[0].Split('-');
            char _letter = (char)range_letter[1][0];
            (int Low, int High) _range = (Int32.Parse(range_str[0]), Int32.Parse(range_str[1]));
            (ValueTuple<int, int> range, char letter) _parsed = (_range, _letter);
            return _parsed;
        }

        private int CountOccurence(char c, string pwd)
        {
            int i = 0;
            foreach (char x in pwd)
            {
                if (x == c)
                {
                    i++;
                }
            }
            return i;
        }
    }
}
