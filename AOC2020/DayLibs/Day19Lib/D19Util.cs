using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using AOC2020.AOCInput;

namespace AOC2020.DayLibs.Day19Lib
{
    public static class D19Util
    {

        public static string[] GetMessages(string day="19", string part="1")
        {
            string messagesRaw = InputParser.ReadChunksToStringArr(day, part)[1];
            return messagesRaw.Split('\n');
        }

        public static Dictionary<int, string> GetRules(string day="19", string part="1")
        {
            Dictionary<int, string> rules = new Dictionary<int, string>();
            string rulesRaw = InputParser.ReadChunksToStringArr(day, part)[0];
            string[] ruleSplit;

            foreach (string rule in rulesRaw.Split('\n'))
            {
                if (String.IsNullOrEmpty(rule) || String.IsNullOrWhiteSpace(rule))
                { continue;  }

                ruleSplit = rule.Split(':');
                try
                {
                    rules.Add(Int32.Parse(ruleSplit[0].Trim()), ruleSplit[1].Trim());
                }
                catch
                {
                    Console.WriteLine($"'{rule}'");
                    throw;
                }
            }

            return rules;
        }

        public static string GenRegexRule(int ruleNo, Dictionary<int, string> rules)
        {
            string rule = rules[ruleNo];

            if (rule.StartsWith("\""))
            {
                return GetRuleVal(rule);
            }

            string regexRule = "(";
            Regex ruleNumPat = new Regex(@$"\b{ruleNo}\b");
            string[] rSeq = rule.Split('|');

            if (rSeq.Length > 1)
            {
                if (rSeq[1].Contains(ruleNo.ToString()))
                {
                    if (ruleNumPat.IsMatch(rSeq[1]))
                    {
                        rSeq[1] = ruleNumPat.Replace(rSeq[1], $"({rSeq[0].Trim()})");
                    }
                }
            }

            for (int i = 0; i < rSeq.Length; i++)
            {
                foreach (string rNo in rSeq[i].Trim().Split(" "))
                {
                    // This is a bit messy, but basically handles the variety of
                    // cases arising from when the above replacement reults in
                    // one vs a sequence of rule numbers being inserted.

                    if (rNo.StartsWith("(") && rNo.EndsWith(")"))
                    {
                        regexRule += $"({GenRegexRule(Int32.Parse(rNo[1..^1]), rules)})+";
                    }
                    else if (rNo.StartsWith("("))
                    {
                        regexRule += $"({GenRegexRule(Int32.Parse(rNo[1..]), rules)})+";
                    }
                    else if (rNo.EndsWith(")"))
                    {
                        regexRule += $"({GenRegexRule(Int32.Parse(rNo[0..^1]), rules)}){{1,2}}";
                    }
                    else
                    {
                        regexRule += GenRegexRule(Int32.Parse(rNo), rules);
                    }
                }
                if (i < rSeq.Length - 1)
                {
                    regexRule += "|";
                }
            }

            regexRule += ")";

            return regexRule;
        }

        public static string GetRuleVal(string rule)
        {
            return rule.Split("\"")[1];
        }

    }
}
