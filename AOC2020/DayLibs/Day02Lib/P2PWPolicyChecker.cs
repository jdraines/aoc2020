using System;
using ParsedPolicy = System.ValueTuple<System.ValueTuple<int, int>, char>;

namespace AOC2020.DayLibs.Day02Lib
{
    public class P2PWPolicyChecker : IPWPolicyChecker
    {
        public P2PWPolicyChecker()
        {
        }

        public bool PwdIsValid(string policy, string pwd)
        {
            ParsedPolicy pPolicy = ParsePolicy(policy);
            int pos1 = pPolicy.Item1.Item1;
            int pos2 = pPolicy.Item1.Item2;
            return EvalXORCharPosition(pPolicy.Item2, pwd, pos1, pos2);
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

        private bool EvalXORCharPosition(char c, string pwd, int pos1, int pos2)
        {
            return CharAtIndex(c, pwd, pos1) ^ CharAtIndex(c, pwd, pos2);
        }

        private bool CharAtIndex(char c, string pwd, int idx)
        {
            return pwd[idx] == c;
        }
    }
}
