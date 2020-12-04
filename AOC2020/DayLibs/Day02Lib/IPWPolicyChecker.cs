using System;
namespace AOC2020.DayLibs.Day02Lib
{
    public interface IPWPolicyChecker
    {
        public bool PwdIsValid(string policy, string pwd);
    }
}
