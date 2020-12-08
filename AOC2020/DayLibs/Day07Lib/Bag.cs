using System;

namespace AOC2020.DayLibs.Day07Lib
{
    public struct Bag
    {
        public string Descriptor;
        public string Color;
        public string Name;

        public Bag(string descriptor, string color)
        {
            Descriptor = descriptor;
            Color = color;
            Name = $"{descriptor}_{color}";
        }

        public override string ToString()
        {
            return "<Bag::\n" +
                  $"    descriptor={Descriptor}\n" +
                  $"    color={Color}>";
        }

        public static bool operator ==(Bag a, Bag b)
        {
            return a.Descriptor == b.Descriptor && a.Color == b.Color;
        }

        public static bool operator !=(Bag a, Bag b)
        {
            return a.Descriptor != b.Descriptor || a.Color != b.Color;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Bag obj)
        {
            return this == obj;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
