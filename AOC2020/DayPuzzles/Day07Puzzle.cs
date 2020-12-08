using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;
using LuggageRule = AOC2020.DayLibs.Day07Lib.LuggageRule;
using Bag = AOC2020.DayLibs.Day07Lib.Bag;
using BigInt = System.Numerics.BigInteger;

namespace AOC2020.DayPuzzles
{
    public class Day07Puzzle : IDayPuzzle
    {
        public string Day { get; } = "07";

        public void SolvePart1()
        {
            string part1 = "1";
            List<LuggageRule> LugRules = GetLuggageRules(part: part1);
            HashSet<Bag> Containers = new HashSet<Bag>();
            Bag myBag = new Bag("shiny", "gold");

            List<Bag> bagsToCheck = new List<Bag>() { myBag };
            do
            {
                bagsToCheck = CheckBags(bagsToCheck, ref Containers, LugRules);
            } while (bagsToCheck.Count > 0);

            HashSet<string> colors = Containers.Select(b => b.Name).ToHashSet();

            Console.WriteLine($"Number of possible container colors: {colors.Count}");
        }

        public void SolvePart2()
        {
            string part2 = "1";
            List<LuggageRule> LugRules = GetLuggageRules(part: part2);
            Bag myBag = new Bag("shiny", "gold");
            List<Bag> bags = CountContainedBags(myBag, LugRules);
            Console.WriteLine($"Count of bags: {bags.Count}");
        }

        public void SolveTest()
        {
            string test = "Test";
            List<LuggageRule> LugRules = GetLuggageRules(part: test);
            Bag myBag = new Bag("shiny", "gold");
            List<Bag> bags = CountContainedBags(myBag, LugRules);
            Console.WriteLine($"Count of bags: {bags.Count}");
        }

        private List<LuggageRule> GetLuggageRules (string part)
        {
            string[] rules = InputParser.ReadToStringArr(Day, part);

            List<LuggageRule> LugRules = new List<LuggageRule>();

            foreach (string rule in rules)
            {
                LuggageRule r = new LuggageRule(rule);
                LugRules.Add(r);
            }
            return LugRules;
        }

        private bool BagInChildTypes(Bag bag, LuggageRule rule)
        {
            foreach(Bag btype in rule.childTypes)
            {
                if (btype == bag)
                {
                    return true;
                }
            }
            return false;
        }

        private List<Bag> CheckBags(List<Bag> bagsToCheck, ref HashSet<Bag> checkedBags, List<LuggageRule> LugRules)
        {
            List<Bag> bagsToCheckNext = new List<Bag>();
            foreach (Bag bag in bagsToCheck)
            {
                foreach (LuggageRule rule in LugRules)
                {
                    if (BagInChildTypes(bag, rule))
                    {
                        if (!checkedBags.Contains(rule.parent))
                        {
                            checkedBags.Add(rule.parent);
                            bagsToCheckNext.Add(rule.parent);
                        }
                    }
                }
            }
            return bagsToCheckNext;
        }

        private LuggageRule GetRule(Bag bag, List<LuggageRule> LugRules)
        {
            return LugRules.Where(r => r.parent == bag).First();
        }

        private List<Bag> CountContainedBags(Bag bag, List<LuggageRule> rules)
        {
            List<Bag> bags = new List<Bag>();

            LuggageRule rule = GetRule(bag, rules);
            Console.WriteLine(rule);

            if (rule.childTypes.Count > 0)
            {
                foreach ((Bag bg, int ct) in rule.children)
                {
                    for (int i=0; i < ct; i++)
                    {
                        bags.Add(bg);
                        bags.AddRange(CountContainedBags(bg, rules));
                    }
                }
            }

            return bags;
        }
    }
}
