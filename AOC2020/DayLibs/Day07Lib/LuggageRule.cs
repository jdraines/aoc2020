using System;
using System.Linq;
using System.Collections.Generic;
using BagGroup = System.Tuple<AOC2020.DayLibs.Day07Lib.Bag, int>;

namespace AOC2020.DayLibs.Day07Lib
{
    public class LuggageRule
    {
        public Bag parent;
        public List<BagGroup> children;
        public List<Bag> childTypes;

        public LuggageRule(string ruleStr)
        {
            parent = Baggify(ruleStr.Split("bags contain")[0].Trim());
            children = ListChildren(ruleStr.Split("bags contain")[1].Trim()[0..^1]);
            childTypes = children.Select(c => c.Item1).ToList();
        }

        public override string ToString()
        {
            string sep = ",\n";
            return "<Luggage Rule::\n" +
                  $"    parent={parent.ToString()}\n" +
                  $"    children={string.Join(sep, children)}>";
        }

        public static bool operator ==(LuggageRule a, LuggageRule b)
        {
            return a.parent == b.parent &&
                   a.children == b.children;
        }

        public static bool operator !=(LuggageRule a, LuggageRule b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(LuggageRule obj)
        {
            try
            {
                return this == obj;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Tuple.Create(parent, children).GetHashCode();
        }

        private Bag Baggify(string bagStr)
        {
            string[] bag = bagStr.Split(' ');
            return new Bag(bag[0], bag[1]);
        }

        private List<BagGroup> ListChildren(string childStr)
        {
            List<BagGroup> childBags = new List<BagGroup>();

            if (childStr == "no other bags")
            {
                return childBags;
            }

            string[] bagStrs = childStr.Split(", ");
            foreach (string bagStr in bagStrs)
            {
                string[] bagBits = bagStr.Split(" ");
                int num = Int32.Parse(bagBits[0]);
                string desc = bagBits[1];
                string color = bagBits[2];
                childBags.Add(new BagGroup(new Bag(desc, color), num));
            }
            return childBags;
        }
    }
}
