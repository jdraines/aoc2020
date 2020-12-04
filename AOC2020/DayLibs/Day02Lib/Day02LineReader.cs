using System;

namespace AOC2020.DayLibs.Day02Lib
{
    public static class Day02LineReader
    {
        public static ValueTuple<string, string> SplitPolicyPwd(string line)
        {
            string[] split = line.Split(':');
            (string policy, string password) _PolPass = (split[0], split[1]);
            return _PolPass;
        }
    }
}
